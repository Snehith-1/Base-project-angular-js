/**=========================================================
 * Module: config.js
 * App routes and resources configuration
 =========================================================*/


(function () {
    'use strict';

    angular
        .module('app.routes')
        .config(routesConfig);

    routesConfig.$inject = ['$stateProvider', '$locationProvider', '$urlRouterProvider', 'RouteHelpersProvider'];
    function routesConfig($stateProvider, $locationProvider, $urlRouterProvider, helper) {

        // Set the following to true to enable the HTML5 Mode
        // You may have to set <base> tag in index and a routing configuration in your server
        $locationProvider.html5Mode(false);

        // defaults to dashboard
        $urlRouterProvider.otherwise('/page/user_login');

        // version update
        var version = '1.0';

        // 
        // Application Routes
        // -----------------------------------   
        $stateProvider
          .state('app', {
              url: '/app',
              abstract: true,
              templateUrl: helper.basepath('app.html?ver=' + version + '"'),
              resolve: helper.resolveFor('fastclick', 'modernizr', 'icons', 'screenfull', 'animo', 'sparklines', 'slimscroll', 'classyloader', 'toaster', 'whirl')
          })
          .state('app.welcome', {
              url: '/welcome',
              title: 'Welcome',
              templateUrl: helper.basepath('welcome.html?ver=' + version + '"')
          })

          .state('page.helpdashboard', {
              url: '/helpdashboard',
              title: 'helpdashboard',
              templateUrl: 'app/pages/helpdashboard.html?ver=' + version + '"',
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'filestyle', 'localytics.directives')
          })

              .state('page.ServiceRequestApproval', {
                  url: '/ServiceRequestApproval',
                  title: 'ServiceRequestApproval',
                  templateUrl: 'app/pages/ServiceRequestApproval.html?ver=' + version + '"',
              })

          .state('app.deploymentadd', {
              url: '/deploymentadd',
              title: 'Deployment',
              templateUrl: helper.basepath('deploymentAdd.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert')
          })
            .state('app.deploymentsummary', {
                url: '/deploymentsummary',
                title: 'Deployment Summary',
                templateUrl: helper.basepath('deploymentSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
            })


          //
          // Single Page Routes
          // -----------------------------------
          .state('page', {
              url: '/page',
              templateUrl: 'app/pages/page.html?ver=' + version + '"',
              resolve: helper.resolveFor('modernizr', 'icons'),
              controller: ['$rootScope', function ($rootScope) {
                  $rootScope.app.layout.isBoxed = false;
              }]
          })

                  .state('app.defapp',
        {
            url: '/defapp',
            title: 'defapp',
            templateUrl: helper.basepath('ems.ecms/defapp.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })


          .state('page.login', {
              url: '/login',
              title: 'Login',
              templateUrl: 'app/pages/login.html?ver=' + version + '"',
          })

             .state('page.user_login', {
                 url: '/user_login',
                 title: 'user_login',
                 templateUrl: 'app/pages/user_login.html?ver=' + version + '"',
             })

          .state('page.lock', {
              url: '/lock',
              title: 'Lock',
              templateUrl: 'app/pages/lock.html?ver=' + version + '"'
          })
           .state('page.404', {
               url: '/404',
               title: 'Not Found',
               templateUrl: 'app/pages/404.html?ver=' + version + '"'
           })
            .state('page.401', {
                url: '/401',
                title: 'Session Expired',
                templateUrl: 'app/pages/401.html?ver=' + version + '"'
            })
             .state('page.500', {
                 url: '/500',
                 title: 'Internal Error',
                 templateUrl: 'app/pages/500.html?ver=' + version + '"'
             })
              .state('page.ssologin', {
                  url: '/sso',
                  title: 'SSO',
                  templateUrl: 'app/pages/ssoLogin.html?ver=' + version + '"',
              })
            .state('page.ssoresponse', {
                url: '/ssoresponse',
                title: 'SSO_response',
                templateUrl: 'app/pages/ssoresponse.html?ver=' + version + '"',
            })
            .state('page.feedback', {
                url: '/feedback',
                title: 'feedback',
                templateUrl: 'app/pages/feedback.html?ver=' + version + '"',
            })
              .state('app.deploymentAdd', {
                  url: '/deploymentAdd',
                  title: 'Add Deployment',
                  templateUrl: helper.basepath('ems.its/deploymentAdd.html?ver=' + version + '"'),
                  resolve: helper.resolveFor('oitozero.ngSweetAlert', 'localytics.directives')
              })
            .state('app.deploymentSummary', {
                url: '/deploymentSummary',
                title: 'Deployment Summary',
                templateUrl: helper.basepath('ems.its/deploymentSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'datatables', 'chartjs')
            })
            .state('app.deploymentEdit', {
                url: '/deploymentEdit',
                title: 'Edit Deployment',
                templateUrl: helper.basepath('ems.its/deploymentEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'localytics.directives')
            })

             .state('app.myApproval',
        {
            url: '/myApproval',
            title: 'myApproval',
            templateUrl: helper.basepath('ems.its/myApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid', 'ngTableExport')

        })

        .state('app.iasnMstTeamManagement', {
            url: '/iasnMstTeamManagement',
            title: 'iasnMstTeamManagement',
            templateUrl: helper.basepath('ems.iasn/iasnMstTeamManagement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        // iasnWomMergeWorkItem


        .state('app.iasnWomMergeWorkItem', {
            url: '/iasnWomMergeWorkItem',
            title: 'iasnWomMergeWorkItem',
            templateUrl: helper.basepath('ems.iasn/iasnWomMergeWorkItem.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.iasnMstTeamAdd', {
            url: '/iasnMstTeamAdd',
            title: 'iasnMstTeamAdd',
            templateUrl: helper.basepath('ems.iasn/iasnMstTeamAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.iasnMstTeamEdit', {
            url: '/iasnMstTeamEdit',
            title: 'iasnMstTeamEdit',
            templateUrl: helper.basepath('ems.iasn/iasnMstTeamEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives')
        })

        .state('app.iasnWomWorkOrderSummary', {
            url: '/iasnWomWorkOrderSummary',
            title: 'iasnWomWorkOrderSummary',
            templateUrl: helper.basepath('ems.iasn/iasnWomWorkOrderSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.iasnTrnMyWorkItemSummary', {
            url: '/iasnTrnMyWorkItemSummary',
            title: 'iasnTrnMyWorkItemSummary',
            templateUrl: helper.basepath('ems.iasn/iasnTrnMyWorkItemSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })


        .state('app.iasnTrnWorkItem360', {
            url: '/iasnTrnWorkItem360',
            title: 'iasnTrnWorkItem360',
            templateUrl: helper.basepath('ems.iasn/iasnTrnWorkItem360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives', 'ngWig')
        })
        .state('app.iasnTrnArchivalSummary', {
            url: '/iasnTrnArchivalSummary',
            title: 'iasnTrnArchivalSummary',
            templateUrl: helper.basepath('ems.iasn/iasnTrnArchivalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig')
        })

        .state('app.iasnMstZonalMapping', {
            url: '/iasnMstZonalMapping',
            title: 'iasnMstZonalMapping',
            templateUrl: helper.basepath('ems.iasn/iasnMstZonalMapping.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig')
        })

        .state('app.iasnTrnMyWorkItem360', {
            url: '/iasnTrnMyWorkItem360',
            title: 'iasnTrnMyWorkItem360',
            templateUrl: helper.basepath('ems.iasn/iasnTrnMyWorkItem360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

              .state('app.iasnTrnCustomerWrkSummary', {
                  url: '/iasnTrnCustomerWrkSummary',
                  title: 'iasnTrnCustomerWrkSummary',
                  templateUrl: helper.basepath('ems.iasn/iasnTrnCustomerWrkSummary.html?ver=' + version + '"'),
                  resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
              })

             .state('app.iasnTrnWorkItemAllotted360', {
                 url: '/iasnTrnWorkItemAllotted360',
                 title: 'iasnTrnWorkItemAllotted360',
                 templateUrl: helper.basepath('ems.iasn/iasnTrnWorkItemAllotted360.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives', 'ngWig')
             })

              .state('app.composeMail', {
                  url: '/composeMail',
                  title: 'composeMail',
                  templateUrl: helper.basepath('ems.iasn/composeMail.html?ver=' + version + '"'),
                  resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig')
              })



          .state('app.serviceApprovalView',
        {
            url: '/serviceApprovalView',
            title: 'serviceApprovalView',
            templateUrl: helper.basepath('ems.its/serviceApprovalView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.departmentApprovalView',
        {
            url: '/departmentApprovalView',
            title: 'departmentApprovalView',
            templateUrl: helper.basepath('ems.its/departmentApprovalView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
              .state('app.collateralsummary',
        {
            url: '/collateralsummary',
            title: 'collateralsummary',
            templateUrl: helper.basepath('ems.ecms/collateralsummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

                   .state('app.collateral',
        {
            url: '/collateral',
            title: 'collateral',
            templateUrl: helper.basepath('ems.ecms/createcollateral.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
        .state('app.reportpagedetails',
        {
            url: '/reportpagedetails',
            title: 'reportpagedetails',
            templateUrl: helper.basepath('ems.ecms/reportpagedetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
              .state('app.editCollateral',
        {
            url: '/editCollateral',
            title: 'editCollateral',
            templateUrl: helper.basepath('ems.ecms/editCollateral.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

.state('app.itDashboard',
        {
            url: '/itDashboard',
            title: 'itDashboard',
            templateUrl: helper.basepath('ems.its/itDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
             .state('app.securitytype',
        {
            url: '/securitytype',
            title: 'securitytype',
            templateUrl: helper.basepath('ems.ecms/addSecurityType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.managementApprovalView',
        {
            url: '/managementApprovalView',
            title: 'managementApprovalView',
            templateUrl: helper.basepath('ems.its/managementApprovalView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

            .state('app.customerAlertHistory',

        {

            url: '/customerAlertHistory',

            title: 'customerAlertHistory',

            templateUrl: helper.basepath('ems.ecms/customerAlertHistory.html?ver=' + version + '"'),

            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')



        })

         .state('app.historyApprovalView',
        {
            url: '/historyApprovalView',
            title: 'historyApprovalView',
            templateUrl: helper.basepath('ems.its/historyApprovalView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

          .state('app.dependencyApprovalView',
        {
            url: '/dependencyApprovalView',
            title: 'dependencyApprovalView',
            templateUrl: helper.basepath('ems.its/dependencyApprovalView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

          .state('app.cacApproval',
        {
            url: '/cacApproval',
            title: 'cacApproval',
            templateUrl: helper.basepath('ems.its/cacApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

                 .state('app.newServiceTicket',
        {
            url: '/newServiceTicket',
            title: 'newServiceTicket',
            templateUrl: helper.basepath('ems.its/newServiceTicket.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.viewTicketDetails',
        {
            url: '/viewTicketDetails',
            title: 'viewTicketDetails',
            templateUrl: helper.basepath('ems.its/viewTicketDetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

      .state('app.viewServiceTicket',
        {
            url: '/viewServiceTicket',
            title: 'viewServiceTicket',
            templateUrl: helper.basepath('ems.its/viewServiceTicket.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

                .state('app.viewMyAsset',
        {
            url: '/viewMyAsset',
            title: 'viewMyAsset',
            templateUrl: helper.basepath('ems.asset/viewMyAsset.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngDialog', 'datatables')

        })

         .state('app.acknowledgeMyAsset',
        {
            url: '/acknowledgeMyAsset',
            title: 'acknowledgeMyAsset',
            templateUrl: helper.basepath('ems.asset/acknowledgeMyAsset.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.assetSurrender',
        {
            url: '/assetSurrender',
            title: 'assetSurrender',
            templateUrl: helper.basepath('ems.asset/assetSurrender.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngDialog', 'datatables')

        })

        .state('app.temporaryHandover',
        {
            url: '/temporaryHandover',
            title: 'temporaryHandover',
            templateUrl: helper.basepath('ems.asset/temporaryHandover.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.dropdown',
        {
            url: '/dropdown',
            title: 'dropdown',
            templateUrl: helper.basepath('ems.asset/dropdown.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

            .state('app.testslide',
        {
            url: '/testslide',
            title: 'testslide',
            templateUrl: helper.basepath('ems.asset/testslide.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.adminlogin',
        {
            url: '/adminlogin',
            title: 'adminlogin',
            templateUrl: helper.basepath('ems.asset/adminlogin.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

       .state('app.assetDashboard',
        {
            url: '/assetDashboard',
            title: 'assetDashboard',
            templateUrl: helper.basepath('ems.asset/assetDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.loanManagement',
        {
            url: '/loanManagement',
            title: 'loanManagement',
            templateUrl: helper.basepath('ems.ecms/loanManagement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'flot-chart', 'flot-chart-plugins', 'weather-icons')

        })
            .state('app.viewloan2deferralDetails', {
                url: '/viewloan2deferralDetails',
                title: 'viewloan2deferralDetails',
                templateUrl: helper.basepath('ems.ecms/viewloan2deferralDetails.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
            })

             .state('app.DeferralManagement',
        {
            url: '/DeferralManagement',
            title: 'DeferralManagement',
            templateUrl: helper.basepath('ems.ecms/deferralManagement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

              .state('app.ecmsDashboard',
        {
            url: '/ecmsDashboard',
            title: 'ecmsDashboard',
            templateUrl: helper.basepath('ems.ecms/ecmsDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

              .state('app.createLoan',
        {
            url: '/createLoan',
            title: 'createLoan',
            templateUrl: helper.basepath('ems.ecms/createLoan.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

            .state('app.createDeferral',
        {
            url: '/createDeferral',
            title: 'createDeferral',
            templateUrl: helper.basepath('ems.ecms/createDeferral.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

            .state('app.rmManagement',
        {
            url: '/rmManagement',
            title: 'rmManagement',
            templateUrl: helper.basepath('ems.ecms/rmManagement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

               .state('app.rmManagementRequest',
        {
            url: '/rmManagementRequest',
            title: 'rmManagementRequest',
            templateUrl: helper.basepath('ems.ecms/rmManagementRequest.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

              .state('app.vertical',
        {
            url: '/vertical',
            title: 'vertical',
            templateUrl: helper.basepath('ems.ecms/vertical.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })

              .state('app.loanMaster',
        {
            url: '/loanMaster',
            title: 'loanMaster',
            templateUrl: helper.basepath('ems.ecms/loanMaster.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })


             .state('app.covenantType',
        {
            url: '/covenantType',
            title: 'covenantType',
            templateUrl: helper.basepath('ems.ecms/covenentType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
              .state('app.transferRM',
        {
            url: '/transferRM',
            title: 'transferRM',
            templateUrl: helper.basepath('ems.ecms/transferRM.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
            .state('app.deferral',
        {
            url: '/deferral',
            title: 'deferral',
            templateUrl: helper.basepath('ems.ecms/deferral.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

             .state('app.editDeferral',
        {
            url: '/editDeferral',
            title: 'editDeferral',
            templateUrl: helper.basepath('ems.ecms/editDeferral.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

                   .state('app.registercustomerEdit',
        {
            url: '/registercustomerEdit',
            title: 'registercustomerEdit',
            templateUrl: helper.basepath('ems.ecms/registercustomerEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

             .state('app.customerMaster', {
                 url: '/customerMaster',
                 title: 'customerMaster',
                 templateUrl: helper.basepath('ems.ecms/customerMaster.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
             })

            .state('app.taggedCustomerList', {
                url: '/taggedCustomerList',
                title: 'taggedCustomerList',
                templateUrl: helper.basepath('ems.ecms/taggedCustomerList.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
            })

              .state('app.taggedNPACustomerList', {
                  url: '/taggedNPACustomerList',
                  title: 'taggedNPACustomerList',
                  templateUrl: helper.basepath('ems.ecms/taggedNPACustomerList.html?ver=' + version + '"'),
                  resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
              })

                        .state('app.checkerApprovalSummary',
        {
            url: '/checkerApprovalSummary',
            title: 'checkerApprovalSummary',
            templateUrl: helper.basepath('ems.ecms/checkerApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

                       .state('app.checkerApprovalView',
        {
            url: '/checkerApprovalView',
            title: 'checkerApprovalView',
            templateUrl: helper.basepath('ems.ecms/checkerApprovalView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })


              .state('app.customerAlert',
        {
            url: '/customerAlert',
            title: 'customerAlert',
            templateUrl: helper.basepath('ems.ecms/customerAlert.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

                  .state('app.customerAlertGenerate',
        {
            url: '/customerAlertGenerate',
            title: 'customerAlertGenerate',
            templateUrl: helper.basepath('ems.ecms/customerAlertGenerate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
        })
              .state('app.mailManagement',
        {
            url: '/mailManagement',
            title: 'mailManagement',
            templateUrl: helper.basepath('ems.ecms/mailManagement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })

        .state('app.sendMailalert',
        {
            url: '/sendMailalert',
            title: 'sendMailalert',
            templateUrl: helper.basepath('ems.ecms/sendMailalert.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
        })
        .state('app.mailHistoryView',
        {
            url: '/mailHistoryView',
            title: 'mailHistoryView',
            templateUrl: helper.basepath('ems.ecms/mailHistoryView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
        })

        .state('app.manageDeferral',
        {
            url: '/manageDeferral',
            title: 'manageDeferral',
            templateUrl: helper.basepath('ems.ecms/manageDeferral.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

         .state('app.manageDeferraladd',
        {
            url: '/manageDeferraladd',
            title: 'manageDeferraladd',
            templateUrl: helper.basepath('ems.ecms/manageDeferraladd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

             .state('app.manageDeferraledit', {
                 url: '/manageDeferraledit',
                 title: 'manageDeferraledit',
                 templateUrl: helper.basepath('ems.ecms/manageDeferraledit.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
             })

              .state('app.manageDeferralview', {
                  url: '/manageDeferralview',
                  title: 'manageDeferralview',
                  templateUrl: helper.basepath('ems.ecms/manageDeferralview.html?ver=' + version + '"'),
                  resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
              })



            .state('app.editLoan', {
                url: '/editLoan',
                title: 'editLoan',
                templateUrl: helper.basepath('ems.ecms/editLoan.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
            })

              .state('app.RMDetails', {
                  url: '/RMDetails',
                  title: 'RMDetails',
                  templateUrl: helper.basepath('ems.ecms/RMDetails.html?ver=' + version + '"'),
                  resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
              })

             .state('app.cadApproval', {
                 url: '/cadApproval',
                 title: 'cadApproval',
                 templateUrl: helper.basepath('ems.ecms/cadApproval.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
             })

              .state('app.reopen', {
                  url: '/reopen',
                  title: 'reopen',
                  templateUrl: helper.basepath('ems.ecms/reopen.html?ver=' + version + '"'),
                  resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
              })

             .state('app.reopenclosed', {
                 url: '/reopenclosed',
                 title: 'reopenclosed',
                 templateUrl: helper.basepath('ems.ecms/reopenclosed.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
             })
                 .state('app.extensionApproval', {
                     url: '/extensionApproval',
                     title: 'extensionApproval',
                     templateUrl: helper.basepath('ems.ecms/extensionApproval.html?ver=' + version + '"'),
                     resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
                 })

            .state('app.viewDeferral', {
                url: '/viewDeferral',
                title: 'viewDeferral',
                templateUrl: helper.basepath('ems.ecms/viewDeferral.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
            })

             .state('app.ReportDetails', {
                 url: '/ReportDetails',
                 title: 'ReportDetails',
                 templateUrl: helper.basepath('ems.ecms/ReportDetails.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
             })

              .state('app.reportView', {
                  url: '/reportView',
                  title: 'reportView',
                  templateUrl: helper.basepath('ems.ecms/reportView.html?ver=' + version + '"'),
                  resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
              })

             .state('app.reportViewdetails', {
                 url: '/reportViewdetails',
                 title: 'reportViewdetails',
                 templateUrl: helper.basepath('ems.ecms/reportViewdetails.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
             })


               .state('app.deferralReport', {
                   url: '/deferralReport',
                   title: 'deferralReport',
                   templateUrl: helper.basepath('ems.ecms/deferralReport.html?ver=' + version + '"'),
                   resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
               })

            .state('app.loan2deferral',
        {
            url: '/loan2deferral',
            title: 'loan2deferral',
            templateUrl: helper.basepath('ems.ecms/loan2deferral.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

          .state('app.addCustomer', {
              url: '/addCustomer',
              title: 'addCustomer',
              templateUrl: helper.basepath('ems.ecms/addCustomer.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
          })

         .state('app.editCustomer', {
             url: '/editCustomer',
             title: 'editCustomer',
             templateUrl: helper.basepath('ems.ecms/editCustomer.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
         })

        .state('app.registerCustomersummary', {
            url: '/registerCustomersummary',
            title: 'registerCustomersummary',
            templateUrl: helper.basepath('ems.ecms/registerCustomersummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })

        .state('app.registerCustomer', {
            url: '/registerCustomer',
            title: 'registerCustomer',
            templateUrl: helper.basepath('ems.ecms/registerCustomer.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
                 .state('app.hrmDashboard',
        {
            url: '/hrmDashboard',
            title: 'hrmDashboard',
            templateUrl: helper.basepath('ems.hrm/hrmDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'chartjs')

        })

         .state('app.myProfile',
        {
            url: '/myProfile',
            title: 'myProfile',
            templateUrl: helper.basepath('ems.hrm/myProfile.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.applyLeave',
        {
            url: '/applyLeave',
            title: 'applyLeave',
            templateUrl: helper.basepath('ems.hrm/applyLeave.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

           .state('app.approveLeave',
        {
            url: '/approveLeave',
            title: 'approveLeave',
            templateUrl: helper.basepath('ems.hrm/approveLeave.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'chartjs')

        })

           .state('app.companyPolicies',
        {
            url: '/companyPolicies',
            title: 'companyPolicies',
            templateUrl: helper.basepath('ems.hrm/companyPolicies.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.holidayCalender',
        {
            url: '/holidayCalender',
            title: 'holidayCalender',
            templateUrl: helper.basepath('ems.hrm/holidayCalender.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'jquery-ui', 'jquery-ui-widgets', 'moment', 'fullcalendar')

        })

        .state('app.monthlyAttendance',
        {
            url: '/monthlyAttendance',
            title: 'monthlyAttendance',
            templateUrl: helper.basepath('ems.hrm/monthlyAttendance.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.myTeam',
        {
            url: '/myTeam',
            title: 'myTeam',
            templateUrl: helper.basepath('ems.hrm/myTeam.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.myTeamEmployeeProfile',
        {
            url: '/myTeamEmployeeProfile',
            title: 'myTeamEmployeeProfile',
            templateUrl: helper.basepath('ems.hrm/myTeamEmployeeProfile.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

          .state('app.myLeave',
        {
            url: '/myLeave',
            title: 'myLeave',
            templateUrl: helper.basepath('ems.hrm/myLeave.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'flot-chart', 'flot-chart-plugins')

        })


        .state('app.gmap',
        {
            url: '/gmap',
            title: 'gmap',
            templateUrl: helper.basepath('ems.hrm/gmap.html?ver=' + version + '"'),
            resolve: helper.resolveFor('loadGoogleMapsJS', function () { return loadGoogleMaps(); }, 'ui.map')

        })

       .state('app.hrmAdminLogin',
        {
            url: '/hrmAdminLogin',
            title: 'hrmAdminLogin',
            templateUrl: helper.basepath('ems.hrm/hrmAdminLogin.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.pageredirect', {
            controller: 'reloadController'
        })
          .state('app.lawyerManagement',
        {
            url: '/lawyerManagement',
            title: 'lawyerManagement',
            templateUrl: helper.basepath('ems.lgl/lawyerManagement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

             .state('app.legalSR360',
        {
            url: '/legalSR360',
            title: 'legalSR360',
            templateUrl: helper.basepath('ems.lgl/legalSR360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular')

        })

               .state('app.legalSRapproval360',
        {
            url: '/legalSRapproval360',
            title: 'legalSRapproval360',
            templateUrl: helper.basepath('ems.lgl/legalSRapproval360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular')

        })

              .state('app.legalSRapprovemgmt360',
        {
            url: '/legalSRapprovemgmt360',
            title: 'legalSRapprovemgmt360',
            templateUrl: helper.basepath('ems.lgl/legalSRapprovemgmt360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.registerLawyer',
        {
            url: '/registerLawyer',
            title: 'registerLawyer',
            templateUrl: helper.basepath('ems.lgl/registerLawyer.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'filestyle')

        })
           .state('app.lawfirmSummary',
        {
            url: '/lawfirmSummary',
            title: 'lawfirmSummary',
            templateUrl: helper.basepath('ems.lgl/lawfirmSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
             .state('app.addLawfirm',
        {
            url: '/addLawfirm',
            title: 'addLawfirm',
            templateUrl: helper.basepath('ems.lgl/addLawfirm.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'filestyle')

        })
        .state('app.requestCompliancesummary',
        {
            url: '/requestCompliancesummary',
            title: 'requestCompliancesummary',
            templateUrl: helper.basepath('ems.lgl/requestCompliancesummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
         .state('app.requestcompliance',
        {
            url: '/requestcompliance',
            title: 'requestcompliance',
            templateUrl: helper.basepath('ems.lgl/requestcompliance.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'filestyle')

        })

             .state('app.legalapprovalmgmt',
        {
            url: '/legalapprovalmgmt',
            title: 'legalapprovalmgmt',
            templateUrl: helper.basepath('ems.lgl/legalapprovalmgmt.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.editRequestcompliance',
        {
            url: '/editRequestcompliance',
            title: 'editRequestcompliance',
            templateUrl: helper.basepath('ems.lgl/editRequestcompliance.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'filestyle')

        })
         .state('app.requestCompliance360',
        {
            url: '/requestCompliance360',
            title: 'requestCompliance360',
            templateUrl: helper.basepath('ems.lgl/requestCompliance360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives', 'filestyle')

        })
         .state('app.complianceManagement',
        {
            url: '/complianceManagement',
            title: 'complianceManagement',
            templateUrl: helper.basepath('ems.lgl/complianceManagement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })
         .state('app.editRegisterlawyer',
        {
            url: '/editRegisterlawyer',
            title: 'editRegisterlawyer',
            templateUrl: helper.basepath('ems.lgl/editRegisterlawyer.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'filestyle')

        })

                .state('app.editLawfirm',
        {
            url: '/editLawfirm',
            title: 'editLawfirm',
            templateUrl: helper.basepath('ems.lgl/editLawfirm.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'filestyle')

        })
         .state('app.legalSRsummary',
        {
            url: '/legalSRsummary',
            title: 'legalSRsummary',
            templateUrl: helper.basepath('ems.lgl/legalSRsummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

             .state('app.legalSRapproval',
        {
            url: '/legalSRapproval',
            title: 'legalSRapproval',
            templateUrl: helper.basepath('ems.lgl/legalSRapproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })


              .state('app.editLegalSR',
        {
            url: '/editLegalSR',
            title: 'editLegalSR',
            templateUrl: helper.basepath('ems.lgl/editLegalSR.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

             .state('app.raiselegalSR',
        {
            url: '/raiselegalSR',
            title: 'raiselegalSR',
            templateUrl: helper.basepath('ems.lgl/raiselegalSR.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })
         .state('app.viewLawfirm',
        {
            url: '/viewLawfirm',
            title: 'viewLawfirm',
            templateUrl: helper.basepath('ems.lgl/viewLawfirm.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
        .state('app.viewLawyer',
        {
            url: '/viewLawyer',
            title: 'viewLawyer',
            templateUrl: helper.basepath('ems.lgl/viewLawyer.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
          .state('app.requestComplianceview',
        {
            url: '/requestComplianceview',
            title: 'requestComplianceview',
            templateUrl: helper.basepath('ems.lgl/requestComplianceview.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

             .state('app.penalityAlert',
        {
            url: '/penalityAlert',
            title: 'penalityAlert',
            templateUrl: helper.basepath('ems.ecms/penalityAlert.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.penalityAlertView',
        {
            url: '/penalityAlertView',
            title: 'penalityAlertView',
            templateUrl: helper.basepath('ems.ecms/penalityAlertView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.lawyerPayment',
        {
            url: '/lawyerPayment',
            title: 'lawyerPayment',
            templateUrl: helper.basepath('ems.lgl/lawyerPayment.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

           .state('app.lawyerPaymentView',
        {
            url: '/lawyerPaymentView',
            title: 'lawyerPaymentView',
            templateUrl: helper.basepath('ems.lgl/lawyerPaymentView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
         .state('app.misDataimport',
        {
            url: '/misDataimport',
            title: 'misDataimport',
            templateUrl: helper.basepath('ems.lgl/misDataimport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
           .state('app.customer2misdata',
        {
            url: '/customer2misdata',
            title: 'customer2misdata',
            templateUrl: helper.basepath('ems.lgl/customer2misdata.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
            .state('app.dnTracker',
        {
            url: '/dnTracker',
            title: 'dnTracker',
            templateUrl: helper.basepath('ems.lgl/dnTracker.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
            .state('app.dnCustomer2loandetails',
        {
            url: '/dnCustomer2loandetails',
            title: 'dnCustomer2loandetails',
            templateUrl: helper.basepath('ems.lgl/dnCustomer2loandetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')

        })
             .state('app.dn2Customer2loandetails',
        {
            url: '/dn2Customer2loandetails',
            title: 'dn2Customer2loandetails',
            templateUrl: helper.basepath('ems.lgl/dn2Customer2loandetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')

        })
             .state('app.dn3Customer2loandetails',
        {
            url: '/dn3Customer2loandetails',
            title: 'dn3Customer2loandetails',
            templateUrl: helper.basepath('ems.lgl/dn3Customer2loandetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')

        })

         .state('app.legalSLNmgmt',
        {
            url: '/legalSLNmgmt',
            title: 'legalSLNmgmt',
            templateUrl: helper.basepath('ems.lgl/legalSLNmgmt.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
         .state('app.viewlegalSR',
        {
            url: '/viewlegalSR',
            title: 'viewlegalSR',
            templateUrl: helper.basepath('ems.lgl/viewlegalSR.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })


        .state('app.sanctionManagement',
        {
            url: '/sanctionManagement',
            title: 'sanctionManagement',
            templateUrl: helper.basepath('ems.rsk/sanctionManagement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
            .state('app.Customer2EscrowSummary',
        {
            url: '/Customer2EscrowSummary',
            title: 'Customer2EscrowSummary',
            templateUrl: helper.basepath('ems.rsk/Customer2EscrowSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })



         .state('app.sanctionAdd',
        {
            url: '/sanctionAdd',
            title: 'sanctionAdd',
            templateUrl: helper.basepath('ems.rsk/sanctionAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

      .state('app.sanctionCreate',
        {
            url: '/sanctionCreate',
            title: 'sanctionCreate',
            templateUrl: helper.basepath('ems.rsk/sanctionCreate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

        .state('app.sanctionEdit',
        {
            url: '/sanctionEdit',
            title: 'sanctionEdit',
            templateUrl: helper.basepath('ems.rsk/sanctionEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })
       .state('app.sanctionView',
        {
            url: '/sanctionView',
            title: 'sanctionView',
            templateUrl: helper.basepath('ems.rsk/sanctionView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

         .state('app.documentation',
        {
            url: '/documentation',
            title: 'documentation',
            templateUrl: helper.basepath('ems.rsk/documentation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

          .state('app.dasTracker',
        {
            url: '/dasTracker',
            title: 'dasTracker',
            templateUrl: helper.basepath('ems.rsk/dasTracker.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.dasRemitterBuyers',
        {
            url: '/dasRemitterBuyers',
            title: 'dasRemitterBuyers',
            templateUrl: helper.basepath('ems.rsk/dasRemitterBuyers.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

                .state('app.customerManagement',
        {
            url: '/customerManagement',
            title: 'customerManagement',
            templateUrl: helper.basepath('ems.rsk/customerManagement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.customerManagement360',
        {
            url: '/customerManagement360',
            title: 'customerManagement360',
            templateUrl: helper.basepath('ems.rsk/customerManagement360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.lglDashboard',
        {
            url: '/lglDashboard',
            title: 'lglDashboard',
            templateUrl: helper.basepath('ems.lgl/lglDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

           .state('app.rskDashboard',
        {
            url: '/rskDashboard',
            title: 'rskDashboard',
            templateUrl: helper.basepath('ems.rsk/rskDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'chartjs')

        })
          .state('app.optDashboard',
        {
            url: '/optDashboard',
            title: 'optDashboard',
            templateUrl: helper.basepath('ems.lgl/optDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

          .state('app.rmMapping',
        {
            url: '/rmMapping',
            title: 'rmMapping',
            templateUrl: helper.basepath('ems.rsk/rmMapping.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

        .state('app.caseAllocation',
        {
            url: '/caseAllocation',
            title: 'caseAllocation',
            templateUrl: helper.basepath('ems.rsk/caseAllocation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular')

        })

        .state('app.allocationCreate',
        {
            url: '/allocationCreate',
            title: 'allocationCreate',
            templateUrl: helper.basepath('ems.rsk/allocationCreate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

        .state('app.allocationView',
        {
            url: '/allocationView',
            title: 'allocationView',
            templateUrl: helper.basepath('ems.rsk/allocationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.rmAllocation',
        {
            url: '/rmAllocation',
            title: 'rmAllocation',
            templateUrl: helper.basepath('ems.rsk/rmAllocation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

          .state('app.rmAllocationTransfer',
        {
            url: '/rmAllocationTransfer',
            title: 'rmAllocationTransfer',
            templateUrl: helper.basepath('ems.rsk/rmAllocationTransfer.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

            .state('app.rmVisitReport',
        {
            url: '/rmVisitReport',
            title: 'rmVisitReport',
            templateUrl: helper.basepath('ems.rsk/rmVisitReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'jquery-ui', 'jquery-ui-widgets', 'moment', 'fullcalendar')

        })

         .state('app.visitReportGenerate',
        {
            url: '/visitReportGenerate',
            title: 'visitReportGenerate',
            templateUrl: helper.basepath('ems.rsk/visitReportGenerate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives')

        })


        .state('app.externalRegister',
        {
            url: '/externalRegister',
            title: 'externalRegister',
            templateUrl: helper.basepath('ems.rsk/externalRegister.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

           .state('app.externalRegisterAdd',
        {
            url: '/externalRegisterAdd',
            title: 'externalRegisterAdd',
            templateUrl: helper.basepath('ems.rsk/externalRegisterAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

            .state('app.externalRegisterView',
        {
            url: '/externalRegisterView',
            title: 'externalRegisterView',
            templateUrl: helper.basepath('ems.rsk/externalRegisterView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

            .state('app.externalRegisterEdit',
        {
            url: '/externalRegisterEdit',
            title: 'externalRegisterEdit',
            templateUrl: helper.basepath('ems.rsk/externalRegisterEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.allocationCreateDirect',
        {
            url: '/allocationCreateDirect',
            title: 'allocationCreateDirect',
            templateUrl: helper.basepath('ems.rsk/allocationCreateDirect.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

         .state('app.visitReportdetailView',
        {
            url: '/visitReportdetailView',
            title: 'visitReportdetailView',
            templateUrl: helper.basepath('ems.rsk/visitReportdetailView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular')

        })

          .state('app.allocation360',
        {
            url: '/allocation360',
            title: 'allocation360',
            templateUrl: helper.basepath('ems.rsk/allocation360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.allocationHistorydetails',
        {
            url: '/allocationHistorydetails',
            title: 'allocationHistorydetails',
            templateUrl: helper.basepath('ems.rsk/allocationHistorydetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

            .state('app.allocationHistoryView',
        {
            url: '/allocationHistoryView',
            title: 'allocationHistoryView',
            templateUrl: helper.basepath('ems.rsk/allocationHistoryView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular')

        })

          .state('app.allocationZonalRM',
        {
            url: '/allocationZonalRM',
            title: 'allocationZonalRM',
            templateUrl: helper.basepath('ems.rsk/allocationZonalRM.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'jquery-ui', 'jquery-ui-widgets', 'moment', 'fullcalendar')

        })

        .state('app.zonalAllocation360',
        {
            url: '/zonalAllocation360',
            title: 'zonalAllocation360',
            templateUrl: helper.basepath('ems.rsk/zonalAllocation360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

           .state('app.allocationTransfer',
        {
            url: '/allocationTransfer',
            title: 'allocationTransfer',
            templateUrl: helper.basepath('ems.rsk/allocationTransfer.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

          .state('app.allocationTransferInitiate',
        {
            url: '/allocationTransferInitiate',
            title: 'allocationTransferInitiate',
            templateUrl: helper.basepath('ems.rsk/allocationTransferInitiate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.transferApproval',
        {
            url: '/transferApproval',
            title: 'transferApproval',
            templateUrl: helper.basepath('ems.rsk/transferApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.transferApproval360',
        {
            url: '/transferApproval360',
            title: 'transferApproval360',
            templateUrl: helper.basepath('ems.rsk/transferApproval360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

      .state('app.myZonalCustomer',
        {
            url: '/myZonalCustomer',
            title: 'myZonalCustomer',
            templateUrl: helper.basepath('ems.rsk/myZonalCustomer.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.allocationZonalCreate',
        {
            url: '/allocationZonalCreate',
            title: 'allocationZonalCreate',
            templateUrl: helper.basepath('ems.rsk/allocationZonalCreate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

       .state('app.allocationZonalCreateDirect',
        {
            url: '/allocationZonalCreateDirect',
            title: 'allocationZonalCreateDirect',
            templateUrl: helper.basepath('ems.rsk/allocationZonalCreateDirect.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })




           .state('app.raiseSR2authentication',
        {
            url: '/raiseSR2authentication',
            title: 'raiseSR2authentication',
            templateUrl: helper.basepath('ems.lgl/raiseSR2authentication.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

          .state('app.zonalMapping',
        {
            url: '/zonalMapping',
            title: 'zonalMapping',
            templateUrl: helper.basepath('ems.rsk/zonalMapping.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })
        .state('app.dnTrackerCBO',
        {
            url: '/dnTrackerCBO',
            title: 'dnTrackerCBO',
            templateUrl: helper.basepath('ems.lgl/dnTrackerCBO.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.cboCustomer2loandetails',
        {
            url: '/cboCustomer2loandetails',
            title: 'cboCustomer2loandetails',
            templateUrl: helper.basepath('ems.lgl/cboCustomer2loandetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig')
        })
         .state('app.cadReport', {
             url: '/cadReport',
             title: 'cadReport',
             templateUrl: helper.basepath('ems.ecms/cadReport.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })
         .state('app.userReport', {
             url: '/userReport',
             title: 'userReport',
             templateUrl: helper.basepath('ems.ecms/userReport.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })


    .state('app.dnTrackerReport', {
        url: '/dnTrackerReport',
        title: 'dnTrackerReport',
        templateUrl: helper.basepath('ems.lgl/dnTrackerReport.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
    })



             // document drafting

              .state('app.idasMstDocMaster',
        {
            url: '/idasMstDocMaster',
            title: 'idasMstDocMaster',
            templateUrl: helper.basepath('ems.idas/idasMstDocMaster.html'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

             .state('app.idasMstWaterMarkMaster',
        {
            url: '/idasMstWaterMarkMaster',
            title: 'idasMstWaterMarkMaster',
            templateUrl: helper.basepath('ems.idas/idasMstWaterMarkMaster.html'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

             .state('app.idasDocumentSummary',
        {
            url: '/idasDocumentSummary',
            title: 'idasDocumentSummary',
            templateUrl: helper.basepath('ems.idas/idasDocumentSummary.html'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

             .state('app.idasCreateDocument',
        {
            url: '/idasCreateDocument',
            title: 'idasCreateDocument',
            templateUrl: helper.basepath('ems.idas/idasCreateDocument.html'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })


             .state('app.idaspreviewDocument',
        {
            url: '/idaspreviewDocument',
            title: 'idaspreviewDocument',
            templateUrl: helper.basepath('ems.idas/idaspreviewDocument.html'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })
            //end

          .state('app.visitReportCancel',
        {
            url: '/visitReportCancel',
            title: 'visitReportCancel',
            templateUrl: helper.basepath('ems.rsk/visitReportCancel.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngWig', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives')

        })

          .state('app.transferApprovalHistory',
        {
            url: '/transferApprovalHistory',
            title: 'transferApprovalHistory',
            templateUrl: helper.basepath('ems.rsk/transferApprovalHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngWig', 'ngDialog', 'datatables')

        })
        .state('app.lsaManagement',
        {
            url: '/lsaManagement',
            title: 'lsaManagement',
            templateUrl: helper.basepath('ems.idas/lsaManagement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })
             .state('app.IdasDashboard',
        {
            url: '/IdasDashboard',
            title: 'IdasDashboard',
            templateUrl: helper.basepath('ems.idas/IdasDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
           .state('app.createLSA',
        {
            url: '/createLSA',
            title: 'createLSA',
            templateUrl: helper.basepath('ems.idas/createLSA.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })
          .state('app.lsaManagementadd',
        {
            url: '/lsaManagementadd',
            title: 'lsaManagementadd',
            templateUrl: helper.basepath('ems.idas/lsaManagementadd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'parsley')

        })

        //IDAS


           //IDAS
            .state('app.idasTrnRmResponseDoc',
        {
            url: '/idasTrnRmResponseDoc',
            title: 'idasTrnRmResponseDoc',
            templateUrl: helper.basepath('ems.idas/idasTrnRmResponseDoc.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')

        })

        .state('app.idasTrnRetrievalReqCreate',
        {
            url: '/idasTrnRetrievalReqCreate',
            title: 'idasTrnRetrievalReqCreate',
            templateUrl: helper.basepath('ems.idas/idasTrnRetrievalReqCreate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })
        .state('app.idasTrnRetrievalReqView',
        {
            url: '/idasTrnRetrievalReqView',
            title: 'idasTrnRetrievalReqView',
            templateUrl: helper.basepath('ems.idas/idasTrnRetrievalReqView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })
        .state('app.idasTrnRetrievalReqSummary',
        {
            url: '/idasTrnRetrievalReqSummary',
            title: 'idasTrnRetrievalReqSummary',
            templateUrl: helper.basepath('ems.idas/idasTrnRetrievalReqSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })


              .state('app.idasTrnRmResponse',
        {
            url: '/idasTrnRmResponse',
            title: 'idasTrnRmResponse',
            templateUrl: helper.basepath('ems.idas/idasTrnRmResponse.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')

        })
            .state('app.idasTrnPhyDocSummary',
        {
            url: '/idasTrnPhyDocSummary',
            title: 'idasTrnPhyDocSummary',
            templateUrl: helper.basepath('ems.idas/idasTrnPhyDocSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')

        })
             .state('app.idasTrnRmResponseSummary',
        {
            url: '/idasTrnRmResponseSummary',
            title: 'idasTrnRmResponseSummary',
            templateUrl: helper.basepath('ems.idas/idasTrnRmResponseSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')

        })
            .state('app.idasTrnPhyDocVerification',
        {
            url: '/idasTrnPhyDocVerification',
            title: 'idasTrnPhyDocVerification',
            templateUrl: helper.basepath('ems.idas/idasTrnPhyDocVerification.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')

        })
             .state('app.idasTrnPhyDocConversation',
        {
            url: '/idasTrnPhyDocConversation',
            title: 'idasTrnPhyDocConversation',
            templateUrl: helper.basepath('ems.idas/idasTrnPhyDocConversation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')

        })
            .state('app.idasTrnDocConversationMkr',
        {
            url: '/idasTrnDocConversationMkr',
            title: 'idasTrnDocConversationMkr',
            templateUrl: helper.basepath('ems.idas/idasTrnDocConversationMkr.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')

        })
             .state('app.idasTrnDocConversationChkr',
        {
            url: '/idasTrnDocConversationChkr',
            title: 'idasTrnDocConversationChkr',
            templateUrl: helper.basepath('ems.idas/idasTrnDocConversationChkr.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')

        })
                  .state('app.idasTrnMakerSummary',
        {
            url: '/idasTrnMakerSummary',
            title: 'idasTrnMakerSummary',
            templateUrl: helper.basepath('ems.idas/idasTrnMakerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
            .state('app.idasTrnDocVerifyChkr',
        {
            url: '/idasTrnDocVerifyChkr',
            title: 'idasTrnDocVerifyChkr',
            templateUrl: helper.basepath('ems.idas/idasTrnDocVerifyChkr.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

              .state('app.idasTrnDocVerifyMkr',
        {
            url: '/idasTrnDocVerifyMkr',
            title: 'idasTrnDocVerifyMkr',
            templateUrl: helper.basepath('ems.idas/idasTrnDocVerifyMkr.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')

        })

              .state('app.idasTrnCheckerSummary',
        {
            url: '/idasTrnCheckerSummary',
            title: 'idasTrnCheckerSummary',
            templateUrl: helper.basepath('ems.idas/idasTrnCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
                .state('app.idasMstSanctionEdit',
        {
            url: '/idasMstSanctionEdit',
            title: 'idasMstSanctionEdit',
            templateUrl: helper.basepath('ems.idas/idasMstSanctionEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

            .state('app.lsa',
        {
            url: '/lsa',
            title: 'lsa',
            templateUrl: helper.basepath('ems.idas/lsa.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

          .state('app.idasTrnSanctionMgmt',
        {
            url: '/idasTrnSanctionMgmt',
            title: 'idasTrnSanctionMgmt',
            templateUrl: helper.basepath('ems.idas/idasTrnSanctionMgmt.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.createdespatch',
        {
            url: '/createdespatch',
            title: 'createdespatch',
            templateUrl: helper.basepath('ems.idas/createdespatch.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

               .state('app.idasTrnFile2Despatch',
        {
            url: '/idasTrnFile2Despatch',
            title: 'idasTrnFile2Despatch',
            templateUrl: helper.basepath('ems.idas/idasTrnFile2Despatch.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

             .state('app.IdasTrnBatchView',
        {
            url: '/IdasTrnBatchView',
            title: 'IdasTrnBatchView',
            templateUrl: helper.basepath('ems.idas/IdasTrnBatchView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })
            .state('app.IdasTrnBoxDtlsView',
        {
            url: '/IdasTrnBoxDtlsView',
            title: 'IdasTrnBoxDtlsView',
            templateUrl: helper.basepath('ems.idas/IdasTrnBoxDtlsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

              .state('app.IdasTrnDespatchDtlsView',
        {
            url: '/IdasTrnDespatchDtlsView',
            title: 'IdasTrnDespatchDtlsView',
            templateUrl: helper.basepath('ems.idas/IdasTrnDespatchDtlsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })
             .state('app.IdasTrnDespatchBoxDtlsView',
        {
            url: '/IdasTrnDespatchBoxDtlsView',
            title: 'IdasTrnDespatchBoxDtlsView',
            templateUrl: helper.basepath('ems.idas/IdasTrnDespatchBoxDtlsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })


              .state('app.IdasTrnBatchConversationView',
        {
            url: '/IdasTrnBatchConversationView',
            title: 'IdasTrnBatchConversationView',
            templateUrl: helper.basepath('ems.idas/IdasTrnBatchConversationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })


        .state('app.boxdetails',
        {
            url: '/boxdetails',
            title: 'boxdetails',
            templateUrl: helper.basepath('ems.idas/boxdetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

              .state('app.despatchdetails',
        {
            url: '/despatchdetails',
            title: 'despatchdetails',
            templateUrl: helper.basepath('ems.idas/despatchdetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.doc360',
        {
            url: '/doc360',
            title: 'doc360',
            templateUrl: helper.basepath('ems.idas/doc360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.createBox',
        {
            url: '/createBox',
            title: 'createBox',
            templateUrl: helper.basepath('ems.idas/createBox.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

            // docverification

         .state('app.idasTrnSanctionDoc',
        {
            url: '/idasTrnSanctionDoc',
            title: 'idasTrnSanctionDoc',
            templateUrl: helper.basepath('ems.idas/idasTrnSanctionDoc.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')

        })

         .state('app.docverification',
        {
            url: '/docverification',
            title: 'docverification',
            templateUrl: helper.basepath('ems.idas/docverification.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

        .state('app.fileMgmt',
        {
            url: '/fileMgmt',
            title: 'fileMgmt',
            templateUrl: helper.basepath('ems.idas/fileMgmt.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })


        .state('app.boxMgmt',
        {
            url: '/boxMgmt',
            title: 'boxMgmt',
            templateUrl: helper.basepath('ems.idas/boxMgmt.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.despatchMgmt',
        {
            url: '/despatchMgmt',
            title: 'despatchMgmt',
            templateUrl: helper.basepath('ems.idas/despatchMgmt.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })


        .state('app.docverificationMgmt',
        {
            url: '/docverificationMgmt',
            title: 'docverificationMgmt',
            templateUrl: helper.basepath('ems.idas/docverificationMgmt.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.createFile',
        {
            url: '/createFile',
            title: 'createFile',
            templateUrl: helper.basepath('ems.idas/createFile.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

          .state('app.boxStampupdate',
        {
            url: '/boxStampupdate',
            title: 'boxStampupdate',
            templateUrl: helper.basepath('ems.idas/boxStampupdate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })


        .state('app.idasMstDocList',
        {
            url: '/idasMstDocList',
            title: 'idasMstDocList',
            templateUrl: helper.basepath('ems.idas/idasMstDocList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

        .state('app.idasMstCreateSanction',
        {
            url: '/idasMstCreateSanction',
            title: 'idasMstCreateSanction',
            templateUrl: helper.basepath('ems.idas/idasMstCreateSanction.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })
            .state('app.idasMstSanctionSummary',
        {
            url: '/idasMstSanctionSummary',
            title: 'idasMstSanctionSummary',
            templateUrl: helper.basepath('ems.idas/idasMstSanctionSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })
        .state('app.fileStamprefupdate',
        {
            url: '/fileStamprefupdate',
            title: 'fileStamprefupdate',
            templateUrl: helper.basepath('ems.idas/fileStamprefupdate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

        })

        .state('app.docconversation',
        {
            url: '/docconversation',
            title: 'docconversation',
            templateUrl: helper.basepath('ems.idas/docconversation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngWig', 'ngDialog', 'datatables', 'localytics.directives')

        })
        .state('app.phydocverify',
        {
            url: '/phydocverify',
            title: 'phydocverify',
            templateUrl: helper.basepath('ems.idas/phydocverify.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngWig', 'ngDialog', 'datatables', 'localytics.directives')

        })

        .state('app.rmresponse',
        {
            url: '/rmresponse',
            title: 'rmresponse',
            templateUrl: helper.basepath('ems.idas/rmresponse.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngWig', 'ngDialog', 'datatables', 'localytics.directives')

        })
         .state('app.rmresponsesummary',
        {
            url: '/rmresponsesummary',
            title: 'rmresponsesummary',
            templateUrl: helper.basepath('ems.idas/rmresponsesummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngWig', 'ngDialog', 'datatables', 'localytics.directives')

        })
         .state('app.rmresponsedoc',
        {
            url: '/rmresponsedoc',
            title: 'rmresponsedoc',
            templateUrl: helper.basepath('ems.idas/rmresponsedoc.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngWig', 'ngDialog', 'datatables', 'localytics.directives')

        })

         .state('app.idasTrnSanctionMIS',
        {
            url: '/idasTrnSanctionMIS',
            title: 'idasTrnSanctionMIS',
            templateUrl: helper.basepath('ems.idas/idasTrnSanctionMIS.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

         .state('app.idasTrnSanctionMISEdit',
        {
            url: '/idasTrnSanctionMISEdit',
            title: 'idasTrnSanctionMISEdit',
            templateUrl: helper.basepath('ems.idas/idasTrnSanctionMISEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })

          .state('app.idasTrnSanctionDashboard',
        {
            url: '/idasTrnSanctionDashboard',
            title: 'idasTrnSanctionDashboard',
            templateUrl: helper.basepath('ems.idas/idasTrnSanctionDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
         .state('app.dntatReport', {
             url: '/dntatReport',
             title: 'dntatReport',
             templateUrl: helper.basepath('ems.lgl/dntatReport.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })
          .state('app.idasTrnaddLimitinfo', {
              url: '/idasTrnaddLimitinfo',
              title: 'idasTrnaddLimitinfo',
              templateUrl: helper.basepath('ems.idas/idasTrnaddLimitinfo.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid', 'filestyle')
          })
         .state('app.viewLSA', {
             url: '/viewLSA',
             title: 'viewLSA',
             templateUrl: helper.basepath('ems.idas/viewLSA.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })

        .state('app.SysMstEmployeeSummary', {
            url: '/SysMstEmployeeSummary',
            title: 'SysMstEmployeeSummary',
            templateUrl: helper.basepath('ems.system/SysMstEmployeeSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
        })
            .state('app.SysMstEmployeeAdd', {
                url: '/SysMstEmployeeAdd',
                title: 'SysMstEmployeeAdd',
                templateUrl: helper.basepath('ems.system/SysMstEmployeeAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
            })
            .state('app.SysMstRoleSummary', {
                url: '/SysMstRoleSummary',
                title: 'SysMstRoleSummary',
                templateUrl: helper.basepath('ems.system/SysMstRoleSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
            })
            .state('app.SysMstRoleAdd', {
                url: '/SysMstRoleAdd',
                title: 'SysMstRoleAdd',
                templateUrl: helper.basepath('ems.system/SysMstRoleAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
            })
            .state('app.SysMstEmployeeEdit', {
                url: '/SysMstEmployeeEdit',
                title: 'SysMstEmployeeEdit',
                templateUrl: helper.basepath('ems.system/SysMstEmployeeEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
            })
            .state('app.SysMstEmployeeView', {
                url: '/SysMstEmployeeView',
                title: 'SysMstEmployeeView',
                templateUrl: helper.basepath('ems.system/SysMstEmployeeView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
            })
            .state('app.SysMstEmployeeDeactivate', {
                url: '/SysMstEmployeeDeactivate',
                title: 'SysMstEmployeeDeactivate',
                templateUrl: helper.basepath('ems.system/SysMstEmployeeDeactivate.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
            })
            .state('app.SysMstRoleEdit', {
                url: '/SysMstRoleEdit',
                title: 'SysMstRoleEdit',
                templateUrl: helper.basepath('ems.system/SysMstRoleEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig')
            })
        .state('app.idasTrnlimitInfoEdit', {
            url: '/idasTrnlimitInfoEdit',
            title: 'idasTrnlimitInfoEdit',
            templateUrl: helper.basepath('ems.idas/idasTrnlimitInfoEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid', 'filestyle')
        })
          .state('app.rmScheduleLogDetails', {
              url: '/rmScheduleLogDetails',
              title: 'rmScheduleLogDetails',
              templateUrl: helper.basepath('ems.rsk/rmScheduleLogDetails.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
          })
        .state('app.rmScheduleLogView', {
            url: '/rmScheduleLogView',
            title: 'rmScheduleLogView',
            templateUrl: helper.basepath('ems.rsk/rmScheduleLogView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })
        .state('app.documentCheckList',
        {
            url: '/documentCheckList',
            title: 'documentCheckList',
            templateUrl: helper.basepath('ems.rsk/documentCheckList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
          .state('app.IdasMstSecurityAdd', {
              url: '/IdasMstSecurityAdd',
              title: 'IdasMstSecurityAdd',
              templateUrl: helper.basepath('ems.idas/IdasMstSecurityAdd.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
          })

          .state('app.IdasTrnSecurityEdit', {
              url: '/IdasTrnSecurityEdit',
              title: 'IdasTrnSecurityEdit',
              templateUrl: helper.basepath('ems.idas/IdasTrnSecurityEdit.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
          })
         .state('app.MstDashboard', {
             url: '/MstDashboard',
             title: 'MstDashboard',
             templateUrl: helper.basepath('ems.master/MstDashboard.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })
          .state('app.MstCCMemberSummary', {
              url: '/MstCCMemberSummary',
              title: 'MstCCMemberSummary',
              templateUrl: helper.basepath('ems.master/MstCCMemberSummary.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
          })
       .state('app.IdasMstSanction2loanfacilitytypeadd', {
           url: '/IdasMstSanction2loanfacilitytypeadd',
           title: 'IdasMstSanction2loanfacilitytypeadd',
           templateUrl: helper.basepath('ems.idas/IdasMstSanction2loanfacilitytypeadd.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
       })

                .state('app.rmObservationReport', {
                    url: '/rmObservationReport',
                    title: 'ObservationReport',
                    templateUrl: helper.basepath('ems.rsk/rmObservationReport.html?ver=' + version + '"'),
                    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
                })

         .state('app.rmObservationReportView', {
             url: '/rmObservationReportView',
             title: 'ObservationReportView',
             templateUrl: helper.basepath('ems.rsk/rmObservationReportView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })

            .state('app.observationReportSummary', {
                url: '/observationReportSummary',
                title: 'observationReportSummary',
                templateUrl: helper.basepath('ems.rsk/observationReportSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
            })

         .state('app.observationReportApproval', {
             url: '/observationReportApproval',
             title: 'observationReportApproval',
             templateUrl: helper.basepath('ems.rsk/observationReportApproval.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })

        .state('app.tier1Summary', {
            url: '/tier1Summary',
            title: 'tier1Summary',
            templateUrl: helper.basepath('ems.rsk/tier1Summary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })

        .state('app.tier1Approval', {
            url: '/tier1Approval',
            title: 'tier1Approval',
            templateUrl: helper.basepath('ems.rsk/tier1Approval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })

        .state('app.tier2Preparation', {
            url: '/tier2Preparation',
            title: 'tier2Preparation',
            templateUrl: helper.basepath('ems.rsk/tier2Preparation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })

        .state('app.tier2Approval', {
            url: '/tier2Approval',
            title: 'tier2Approval',
            templateUrl: helper.basepath('ems.rsk/tier2Approval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })

       .state('app.tier2ApprovalSummary', {
           url: '/tier2ApprovalSummary',
           title: 'tier2ApprovalSummary',
           templateUrl: helper.basepath('ems.rsk/tier2ApprovalSummary.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
       })

       .state('app.tier2PreparationView', {
           url: '/tier2PreparationView',
           title: 'tier2PreparationView',
           templateUrl: helper.basepath('ems.rsk/tier2PreparationView.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
       })

      .state('app.tier3Preparation', {
          url: '/tier3Preparation',
          title: 'tier3Preparation',
          templateUrl: helper.basepath('ems.rsk/tier3Preparation.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
      })

       .state('app.tier3PreparationView', {
           url: '/tier3PreparationView',
           title: 'tier3PreparationView',
           templateUrl: helper.basepath('ems.rsk/tier3PreparationView.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
       })

        .state('app.tierReport', {
            url: '/tierReport',
            title: 'tierReport',
            templateUrl: helper.basepath('ems.rsk/tierReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })

        .state('app.tierReportView', {
            url: '/tierReportView',
            title: 'tierReportView',
            templateUrl: helper.basepath('ems.rsk/tierReportView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })

         .state('app.tier2Create', {
             url: '/tier2Create',
             title: 'tier2Create',
             templateUrl: helper.basepath('ems.rsk/tier2Create.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })

         .state('app.tier3Create', {
             url: '/tier3Create',
             title: 'tier3Create',
             templateUrl: helper.basepath('ems.rsk/tier3Create.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })

        .state('app.test', {
            url: '/test',
            title: 'test',
            templateUrl: helper.basepath('ems.rsk/test.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })
        .state('app.IdasMstHistorySanctionRefNo', {
            url: '/IdasMstHistorySanctionRefNo',
            title: 'IdasMstHistorySanctionRefNo',
            templateUrl: helper.basepath('ems.idas/IdasMstHistorySanctionRefNo.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })

            .state('app.ZRMObservationReportView', {
                url: '/ZRMObservationReportView',
                title: 'ObservationReportView',
                templateUrl: helper.basepath('ems.rsk/ZRMObservationReportView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
            })


          .state('app.IdasMstSanctionReset', {
              url: '/IdasMstSanctionReset',
              title: 'IdasMstSanctionReset',
              templateUrl: helper.basepath('ems.idas/IdasMstSanctionReset.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
          })
         .state('app.idasTrnLSAapproval', {
             url: '/idasTrnLSAapproval',
             title: 'idasTrnLSAapproval',
             templateUrl: helper.basepath('ems.idas/idasTrnLSAapproval.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })
         .state('app.IdasTrnLSAapprovalview', {
             url: '/IdasTrnLSAapprovalview',
             title: 'IdasTrnLSAapprovalview',
             templateUrl: helper.basepath('ems.idas/IdasTrnLSAapprovalview.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })
        .state('app.MstCCGroupName', {
            url: '/MstCCGroupName',
            title: 'MstCCGroupName',
            templateUrl: helper.basepath('ems.master/MstCCGroupName.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })


        .state('app.exclusionCustomerList', {
            url: '/exclusionCustomerList',
            title: 'exclusionCustomerList',
            templateUrl: helper.basepath('ems.rsk/exclusionCustomerList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })

        .state('app.exclusionZonalCustomerList', {
            url: '/exclusionZonalCustomerList',
            title: 'exclusionZonalCustomerList',
            templateUrl: helper.basepath('ems.rsk/exclusionZonalCustomerList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })
        .state('app.lgl_mst_requesttypesummary', {
            url: '/lgl_mst_requesttypesummary',
            title: 'Request Type ',
            templateUrl: helper.basepath('ems.lgl/lgl_mst_requesttypesummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })
         .state('app.lgl_mst_servicetypesummary', {
             url: '/lgl_mst_servicetypesummary',
             title: 'Service Type ',
             templateUrl: helper.basepath('ems.lgl/lgl_mst_servicetypesummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })
          .state('app.LglMstCompliance2TagLawyer', {
              url: '/LglMstCompliance2TagLawyer',
              title: 'LglMstCompliance2TagLawyer ',
              templateUrl: helper.basepath('ems.lgl/LglMstCompliance2TagLawyer.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid', 'filestyle', 'localytics.directives')
          })
        .state('app.LglTrnInvoiceSummary', {
            url: '/LglTrnInvoiceSummary',
            title: 'LglTrnInvoiceSummary ',
            templateUrl: helper.basepath('ems.lgl/LglTrnInvoiceSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })

         .state('app.LglTrnCompliancePending', {
             url: '/LglTrnCompliancePending',
             title: 'LglTrnCompliancePending ',
             templateUrl: helper.basepath('ems.lgl/LglTrnCompliancePending.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })
        .state('app.LglTrnComplianceCompleted', {
            url: '/LglTrnComplianceCompleted',
            title: 'LglTrnComplianceCompleted ',
            templateUrl: helper.basepath('ems.lgl/LglTrnComplianceCompleted.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })
         .state('app.LglTrnComplianceRejected', {
             url: '/LglTrnComplianceRejected',
             title: 'LglTrnComplianceRejected ',
             templateUrl: helper.basepath('ems.lgl/LglTrnComplianceRejected.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })

         .state('app.idasTrnDocumentUploadMain',
         {
             url: '/idasTrnDocumentUploadMain',
             title: 'idasTrnDocumentUploadMain',
             templateUrl: helper.basepath('ems.idas/idasTrnDocumentUploadMain.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

         })

         .state('app.idasCourierMgmtsummary', {
             url: '/idasCourierMgmtsummary',
             title: 'idasCourierMgmtsummary',
             templateUrl: helper.basepath('ems.idas/idasCourierMgmtsummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })
         .state('app.idasMstCourierCompany',
         {
             url: '/idasMstCourierCompany',
             title: 'idasMstCourierCompany',
             templateUrl: helper.basepath('ems.idas/idasMstCourierCompany.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

         })

         .state('app.idasCourierCreation', {
             url: '/idasCourierCreation',
             title: 'idasCourierCreation',
             templateUrl: helper.basepath('ems.idas/idasCourierCreation.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
         })

         .state('app.idasCourierEdit', {
             url: '/idasCourierEdit',
             title: 'idasCourierEdit',
             templateUrl: helper.basepath('ems.idas/idasCourierEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
         })


         .state('app.idasTrnDocumentUploadChild',
         {
             url: '/idasTrnDocumentUploadChild',
             title: 'idasTrnDocumentUploadChild',
             templateUrl: helper.basepath('ems.idas/idasTrnDocumentUploadChild.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')

         })

         .state('page.CourierMgmtAckForm', {
             url: '/CourierMgmtAckForm',
             title: 'CourierMgmtAckForm',
             templateUrl: 'app/pages/CourierMgmtAckForm.html?ver=' + version + '"',
         })

.state('app.idasTrnCourierView', {
    url: '/idasTrnCourierView',
    title: 'idasTrnCourierView',
    templateUrl: helper.basepath('ems.idas/idasTrnCourierView.html?ver=' + version + '"'),
    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
})

        .state('app.MstCustomerSummary', {
            url: '/MstCustomerSummary',
            title: 'MstCustomerSummary',
            templateUrl: helper.basepath('ems.master/MstCustomerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.MstCustomeradd', {
            url: '/MstCustomeradd',
            title: 'MstCustomeradd',
            templateUrl: helper.basepath('ems.master/MstCustomeradd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'filestyle')
        })
       .state('app.MstInstitutionCustomeradd', {
           url: '/MstInstitutionCustomeradd',
           title: 'MstInstitutionCustomeradd',
           templateUrl: helper.basepath('ems.master/MstInstitutionCustomeradd.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'filestyle')
       })
        .state('app.MstIndividualCustomeradd', {
            url: '/MstIndividualCustomeradd',
            title: 'MstIndividualCustomeradd',
            templateUrl: helper.basepath('ems.master/MstIndividualCustomeradd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'filestyle')
        })
         .state('app.MstAddressType', {
             url: '/MstAddressType',
             title: 'MstAddressType',
             templateUrl: helper.basepath('ems.master/MstAddressType.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
         })
        .state('app.MstUserType', {
            url: '/MstUserType',
            title: 'MstUserType',
            templateUrl: helper.basepath('ems.master/MstUserType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
         .state('app.MstDesignation', {
             url: '/MstDesignation',
             title: 'MstDesignation',
             templateUrl: helper.basepath('ems.master/MstDesignation.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
         })
         .state('app.MstStrategicBusinessUnit', {
             url: '/MstStrategicBusinessUnit',
             title: 'MstStrategicBusinessUnit',
             templateUrl: helper.basepath('ems.master/MstStrategicBusinessUnit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
         })
         .state('app.MstValueChain', {
             url: '/MstValueChain',
             title: 'MstValueChain',
             templateUrl: helper.basepath('ems.master/MstValueChain.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
         })
          .state('app.MstSamunnatiAssociateMaster', {
              url: '/MstSamunnatiAssociateMaster',
              title: 'MstSamunnatiAssociateMaster',
              templateUrl: helper.basepath('ems.master/MstSamunnatiAssociateMaster.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
          })
        .state('app.MstConstitution', {
            url: '/MstConstitution',
            title: 'MstConstitution',
            templateUrl: helper.basepath('ems.master/MstConstitution.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

       .state('app.MstCustomer2userView', {
           url: '/MstCustomer2userView',
           title: 'MstCustomer2userView',
           templateUrl: helper.basepath('ems.master/MstCustomer2userView.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
       })
        .state('app.MstGuarantorSummary', {
            url: '/MstGuarantorSummary',
            title: 'MstGuarantorSummary',
            templateUrl: helper.basepath('ems.master/MstGuarantorSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
            .state('app.MstCustomerView', {
                url: '/MstCustomerView',
                title: 'MstCustomerView',
                templateUrl: helper.basepath('ems.master/MstCustomerView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'filestyle')
            })
         .state('app.MstGuarantorView', {
             url: '/MstGuarantorView',
             title: 'MstGuarantorView',
             templateUrl: helper.basepath('ems.master/MstGuarantorView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
         })
        .state('app.idasSanctionMIS360', {
            url: '/idasSanctionMIS360',
            title: 'idasSanctionMIS360',
            templateUrl: helper.basepath('ems.idas/idasSanctionMIS360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })

     .state('app.tierCustomer360',
        {
            url: '/tierCustomer360',
            title: 'tierCustomer360',
            templateUrl: helper.basepath('ems.rsk/tierCustomer360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')

        })
      .state('app.osdMstBusinessUnit', {
          url: '/osdMstBusinessUnit',
          title: 'osdMstBusinessUnit',
          templateUrl: helper.basepath('ems.osd/osdMstBusinessUnit.html?ver=' + version + '"'),
          resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
      })

        .state('app.osdMstActivitySummary', {
            url: '/osdMstActivitySummary',
            title: 'osdMstActivitySummary',
            templateUrl: helper.basepath('ems.osd/osdMstActivitySummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

            .state('app.osdMstTeamSummary', {
                url: '/osdMstTeamSummary',
                title: 'osdMstTeamSummary',
                templateUrl: helper.basepath('ems.osd/osdMstTeamSummary.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
            })

                .state('app.osdMstDepartmentManagement', {
                    url: '/osdMstDepartmentManagement',
                    title: 'osdMstDepartmentManagement',
                    templateUrl: helper.basepath('ems.osd/osdMstDepartmentManagement.html?ver=' + version + '"'),
                    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
                })

           .state('app.osdTrnServiceRequestAdd',
        {
            url: '/osdTrnServiceRequestAdd',
            title: 'osdTrnServiceRequestAdd',
            templateUrl: helper.basepath('ems.osd/osdTrnServiceRequestAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
            .state('app.osdTrnServiceRequestSummary',
        {
            url: '/osdTrnServiceRequestSummary',
            title: 'osdTrnServiceRequestSummary',
            templateUrl: helper.basepath('ems.osd/osdTrnServiceRequestSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })

         .state('app.osdTrnTicketManagement',
        {
            url: '/osdTrnTicketManagement',
            title: 'osdTrnTicketManagement',
            templateUrl: helper.basepath('ems.osd/osdTrnTicketManagement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })



         .state('app.osdTrnServiceRequestView',
        {
            url: '/osdTrnServiceRequestView',
            title: 'osdTrnServiceRequestView',
            templateUrl: helper.basepath('ems.osd/osdTrnServiceRequestView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

          .state('app.osdTrnTicketManagementView',
        {
            url: '/osdTrnTicketManagementView',
            title: 'osdTrnTicketManagementView',
            templateUrl: helper.basepath('ems.osd/osdTrnTicketManagementView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.LglTrnDNTrackerAE',
        {
            url: '/LglTrnDNTrackerAE',
            title: 'LglTrnDNTrackerAE',
            templateUrl: helper.basepath('ems.lgl/LglTrnDNTrackerAE.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
          .state('app.osdTrnMyActivityAllotted360',
        {
            url: '/osdTrnMyActivityAllotted360',
            title: 'osdTrnMyActivityAllotted360',
            templateUrl: helper.basepath('ems.osd/osdTrnMyActivityAllotted360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.osdTrnMyActivity360',
        {
            url: '/osdTrnMyActivity360',
            title: 'osdTrnMyActivity360',
            templateUrl: helper.basepath('ems.osd/osdTrnMyActivity360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

         .state('app.osdTrnActivityManagement360',
        {
            url: '/osdTrnActivityManagement360',
            title: 'osdTrnActivityManagement360',
            templateUrl: helper.basepath('ems.osd/osdTrnActivityManagement360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

         .state('app.osdDashboard',
        {
            url: '/osdDashboard',
            title: 'osdDashboard',
            templateUrl: helper.basepath('ems.osd/osdDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'chartjs')
        })

              .state('app.osdTrnMyTicket',
        {
            url: '/osdTrnMyTicket',
            title: 'osdTrnMyTicket',
            templateUrl: helper.basepath('ems.osd/osdTrnMyTicket.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

             .state('app.osdTrnServiceRequestTaggedView',
        {
            url: '/osdTrnServiceRequestTaggedView',
            title: 'osdTrnServiceRequestTaggedView',
            templateUrl: helper.basepath('ems.osd/osdTrnServiceRequestTaggedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.customerReport360', {
            url: '/customerReport360',
            title: 'customerReport360',
            templateUrl: helper.basepath('ems.master/customerReport360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
         .state('app.visitdetails360', {
             url: '/visitdetails360',
             title: 'visitdetails360',
             templateUrl: helper.basepath('ems.master/visitdetails360.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
         })
         .state('app.customerReport', {
             url: '/customerReport',
             title: 'customerReport',
             templateUrl: helper.basepath('ems.master/customerReport.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
         })
              .state('app.osdTrnMyActivityTransfer',
        {
            url: '/osdTrnMyActivityTransfer',
            title: 'osdTrnMyActivityTransfer',
            templateUrl: helper.basepath('ems.osd/osdTrnMyActivityTransfer.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
              .state('app.osdTrnServiceRequestForwardView',
        {
            url: '/osdTrnServiceRequestForwardView',
            title: 'osdTrnServiceRequestForwardView',
            templateUrl: helper.basepath('ems.osd/osdTrnServiceRequestForwardView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

              .state('app.osdTrnMyActivityForward360',
        {
            url: '/osdTrnMyActivityForward360',
            title: 'osdTrnMyActivityForward360',
            templateUrl: helper.basepath('ems.osd/osdTrnMyActivityForward360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

                .state('app.osdTrnMyActivityComplete',
        {
            url: '/osdTrnMyActivityComplete',
            title: 'osdTrnMyActivityComplete',
            templateUrl: helper.basepath('ems.osd/osdTrnMyActivityComplete.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.osdTrnMyActivityHistory',
        {
            url: '/osdTrnMyActivityHistory',
            title: 'osdTrnMyActivityHistory',
            templateUrl: helper.basepath('ems.osd/osdTrnMyActivityHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })

        .state('app.osdTrnMyActivityReopenHistory',
        {
            url: '/osdTrnMyActivityReopenHistory',
            title: 'osdTrnMyActivityReopenHistory',
            templateUrl: helper.basepath('ems.osd/osdTrnMyActivityReopenHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
          .state('app.LglTrnDN2GeneratedAE',
        {
            url: '/LglTrnDN2GeneratedAE',
            title: 'LglTrnDN2GeneratedAE',
            templateUrl: helper.basepath('ems.lgl/LglTrnDN2GeneratedAE.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })


          .state('app.LglTrnDNTrackerAEGenerate',
        {
            url: '/LglTrnDNTrackerAEGenerate',
            title: 'LglTrnDNTrackerAEGenerate',
            templateUrl: helper.basepath('ems.lgl/LglTrnDNTrackerAEGenerate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
          .state('app.LglTrnDNTrackerAE2Generate',
        {
            url: '/LglTrnDNTrackerAE2Generate',
            title: 'LglTrnDNTrackerAE2Generate',
            templateUrl: helper.basepath('ems.lgl/LglTrnDNTrackerAE2Generate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
           .state('app.LglTrnDNTrackerAE3Generate',
        {
            url: '/LglTrnDNTrackerAE3Generate',
            title: 'LglTrnDNTrackerAE3Generate',
            templateUrl: helper.basepath('ems.lgl/LglTrnDNTrackerAE3Generate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })

         .state('app.lglTrnDNTrackerFPO',
        {
            url: '/lglTrnDNTrackerFPO',
            title: 'lglTrnDNTrackerFPO',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNTrackerFPO.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
        .state('app.lglTrnDNTrackerRetail',
        {
            url: '/lglTrnDNTrackerRetail',
            title: 'lglTrnDNTrackerRetail',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNTrackerRetail.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
        .state('app.lglTrnDNTrackerOthers',
        {
            url: '/lglTrnDNTrackerOthers',
            title: 'lglTrnDNTrackerOthers',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNTrackerOthers.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })

         .state('app.lglTrnDNTrackerCBO',
        {
            url: '/lglTrnDNTrackerCBO',
            title: 'lglTrnDNTrackerCBO',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNTrackerCBO.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
         .state('app.lglTrnDNTrackerRetailGenerate',
        {
            url: '/lglTrnDNTrackerRetailGenerate',
            title: 'lglTrnDNTrackerRetailGenerate',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNTrackerRetailGenerate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
         .state('app.lglTrnDNTrackerRetail2Generate',
        {
            url: '/lglTrnDNTrackerRetail2Generate',
            title: 'lglTrnDNTrackerRetail2Generate',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNTrackerRetail2Generate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
         .state('app.lglTrnDNTrackerRetail3Generate',
        {
            url: '/lglTrnDNTrackerRetail3Generate',
            title: 'lglTrnDNTrackerRetail3Generate',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNTrackerRetail3Generate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
        .state('app.LglTrnDNTrackerFPOGenerate',
        {
            url: '/LglTrnDNTrackerFPOGenerate',
            title: 'LglTrnDNTrackerFPOGenerate',
            templateUrl: helper.basepath('ems.lgl/LglTrnDNTrackerFPOGenerate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
         .state('app.LglTrnDNTrackerFPO2Generate',
        {
            url: '/LglTrnDNTrackerFPO2Generate',
            title: 'LglTrnDNTrackerFPO2Generate',
            templateUrl: helper.basepath('ems.lgl/LglTrnDNTrackerFPO2Generate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
         .state('app.LglTrnDNTrackerFPO3Generate',
        {
            url: '/LglTrnDNTrackerFPO3Generate',
            title: 'LglTrnDNTrackerFPO3Generate',
            templateUrl: helper.basepath('ems.lgl/LglTrnDNTrackerFPO3Generate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })

         .state('app.lglTrnDNTrackerOthersGenerate',
        {
            url: '/lglTrnDNTrackerOthersGenerate',
            title: 'lglTrnDNTrackerOthersGenerate',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNTrackerOthersGenerate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
         .state('app.lglTrnDNTrackerOthers2Generate',
        {
            url: '/lglTrnDNTrackerOthers2Generate',
            title: 'lglTrnDNTrackerOthers2Generate',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNTrackerOthers2Generate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
         .state('app.lglTrnDNTrackerOthers3Generate',
        {
            url: '/lglTrnDNTrackerOthers3Generate',
            title: 'lglTrnDNTrackerOthers3Generate',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNTrackerOthers3Generate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })

         .state('app.lglTrnDNFPOGenerateTab',
        {
            url: '/lglTrnDNFPOGenerateTab',
            title: 'lglTrnDNFPOGenerateTab',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNFPOGenerateTab.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
         .state('app.lglTrnDNOthersGenerateTab',
        {
            url: '/lglTrnDNOthersGenerateTab',
            title: 'lglTrnDNOthersGenerateTab',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNOthersGenerateTab.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
         .state('app.lglTrnDNRetailGenerateTab',
        {
            url: '/lglTrnDNRetailGenerateTab',
            title: 'lglTrnDNRetailGenerateTab',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNRetailGenerateTab.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
         .state('app.lglTrnDNCBOGenerateTab',
        {
            url: '/lglTrnDNCBOGenerateTab',
            title: 'lglTrnDNCBOGenerateTab',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNCBOGenerateTab.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
         .state('app.lglTrnDNTrackerCBOGenerate',
        {
            url: '/lglTrnDNTrackerCBOGenerate',
            title: 'lglTrnDNTrackerCBOGenerate',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNTrackerCBOGenerate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
         .state('app.lglTrnDNTrackerCBO2Generate',
        {
            url: '/lglTrnDNTrackerCBO2Generate',
            title: 'lglTrnDNTrackerCBO2Generate',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNTrackerCBO2Generate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
         .state('app.lglTrnDNTrackerCBO3Generate',
        {
            url: '/lglTrnDNTrackerCBO3Generate',
            title: 'lglTrnDNTrackerCBO3Generate',
            templateUrl: helper.basepath('ems.lgl/lglTrnDNTrackerCBO3Generate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
        .state('app.LglTrnDNTrackerSkipped',
        {
            url: '/LglTrnDNTrackerSkipped',
            title: 'LglTrnDNTrackerSkipped',
            templateUrl: helper.basepath('ems.lgl/LglTrnDNTrackerSkipped.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives')
        })
        .state('app.MstCustomerEdit',
        {
            url: '/MstCustomerEdit',
            title: 'MstCustomerEdit',
            templateUrl: helper.basepath('ems.master/MstCustomerEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives', 'filestyle')
        })
        .state('app.MstCustomer2userdtlEdit',
        {
            url: '/MstCustomer2userdtlEdit',
            title: 'MstCustomer2userdtlEdit',
            templateUrl: helper.basepath('ems.master/MstCustomer2userdtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'localytics.directives', 'filestyle')
        })
        .state('app.idasTrnDocumentTagging',
        {
            url: '/idasTrnDocumentTagging',
            title: 'idasTrnDocumentTagging',
            templateUrl: helper.basepath('ems.idas/idasTrnDocumentTagging.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })

        .state('app.idasTrnDocumentTaggingView',
        {
            url: '/idasTrnDocumentTaggingView',
            title: 'idasTrnDocumentTaggingView',
            templateUrl: helper.basepath('ems.idas/idasTrnDocumentTaggingView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.idasTrnDocumentTaggingDocView',
        {
            url: '/idasTrnDocumentTaggingDocView',
            title: 'idasTrnDocumentTaggingDocView',
            templateUrl: helper.basepath('ems.idas/idasTrnDocumentTaggingDocView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.idasTrnDocumentTaggingDocChild',
        {
            url: '/idasTrnDocumentTaggingDocChild',
            title: 'idasTrnDocumentTaggingDocChild',
            templateUrl: helper.basepath('ems.idas/idasTrnDocumentTaggingDocChild.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.idasMstDocLabelSummary',
        {
            url: '/idasMstDocLabelSummary',
            title: 'idasMstDocLabelSummary',
            templateUrl: helper.basepath('ems.idas/idasMstDocLabelSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.iasnDashboard',
        {
            url: '/iasnDashboard',
            title: 'iasnDashboard',
            templateUrl: helper.basepath('ems.iasn/iasnDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'morris')
        })
         .state('app.osdTrnApprovalView',
        {
            url: '/osdTrnApprovalView',
            title: 'osdTrnApprovalView',
            templateUrl: helper.basepath('ems.osd/osdTrnApprovalView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

           .state('app.osdTrnApprovalViewHistory',
        {
            url: '/osdTrnApprovalViewHistory',
            title: 'osdTrnApprovalViewHistory',
            templateUrl: helper.basepath('ems.osd/osdTrnApprovalViewHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.allocationReport',
        {
            url: '/allocationReport',
            title: 'allocationReport',
            templateUrl: helper.basepath('ems.rsk/allocationReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
         .state('app.iasnConsolidatedWorkItem',
        {
            url: '/iasnConsolidatedWorkItem',
            title: 'iasnConsolidatedWorkItem',
            templateUrl: helper.basepath('ems.iasn/iasnConsolidatedWorkItem.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
         .state('app.isanconsolidatedview',
        {
            url: '/isanconsolidatedview',
            title: 'isanconsolidatedview',
            templateUrl: helper.basepath('ems.iasn/isanconsolidatedview.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.marketingDashboard',
        {
            url: '/marketingDashboard',
            title: 'marketingDashboard',
            templateUrl: helper.basepath('ems.marketing/marketingDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

         .state('app.osdRptAllTickets',
        {
            url: '/osdRptAllTickets',
            title: 'osdRptAllTickets',
            templateUrl: helper.basepath('ems.osd/osdRptAllTickets.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'ngWig', 'angularGrid', 'localytics.directives')
        })

          .state('app.osdRptAllTicketsView',
        {
            url: '/osdRptAllTicketsView',
            title: 'osdRptAllTicketsView',
            templateUrl: helper.basepath('ems.osd/osdRptAllTicketsView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.osdTrnServiceRequestReopenView',
        {
            url: '/osdTrnServiceRequestReopenView',
            title: 'osdTrnServiceRequestReopenView',
            templateUrl: helper.basepath('ems.osd/osdTrnServiceRequestReopenView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
          .state('app.MstCustomer2tmpuserView',
        {
            url: '/MstCustomer2tmpuserView',
            title: 'MstCustomer2tmpuserView',
            templateUrl: helper.basepath('ems.master/MstCustomer2tmpuserView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
         .state('app.MstCustomertmpView',
        {
            url: '/MstCustomertmpView',
            title: 'MstCustomertmpView',
            templateUrl: helper.basepath('ems.master/MstCustomertmpView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
          .state('app.iasnTrnAllotedSummary',
        {
            url: '/iasnTrnAllotedSummary',
            title: 'iasnTrnAllotedSummary',
            templateUrl: helper.basepath('ems.iasn/iasnTrnAllotedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.iasnTrnPushbackSummary',
        {
            url: '/iasnTrnPushbackSummary',
            title: 'iasnTrnPushbackSummary',
            templateUrl: helper.basepath('ems.iasn/iasnTrnPushbackSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
         .state('app.iasnTrnForwardSummary',
        {
            url: '/iasnTrnForwardSummary',
            title: 'iasnTrnForwardSummary',
            templateUrl: helper.basepath('ems.iasn/iasnTrnForwardSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.iasnTrnCloseSummary',
        {
            url: '/iasnTrnCloseSummary',
            title: 'iasnTrnCloseSummary',
            templateUrl: helper.basepath('ems.iasn/iasnTrnCloseSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.iasnTrnWorkItemSummary',
        {
            url: '/iasnTrnWorkItemSummary',
            title: 'iasnTrnWorkItemSummary',
            templateUrl: helper.basepath('ems.iasn/iasnTrnWorkItemSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
          .state('app.iasnTrnWorkItemMail', {
              url: '/iasnTrnWorkItemMail',
              title: 'iasnTrnWorkItemMail',
              templateUrl: helper.basepath('ems.iasn/iasnTrnWorkItemMail.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives', 'ngWig')
          })
         .state('app.CommonMstDashboard',
        {
            url: '/CommonMstDashboard',
            title: 'CommonMstDashboard',
            templateUrl: helper.basepath('ems.master/CommonMstDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
         .state('app.iasnTrnComposeMail360', {
             url: '/iasnTrnComposeMail360',
             title: 'iasnTrnComposeMail360',
             templateUrl: helper.basepath('ems.iasn/iasnTrnComposeMail360.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives', 'ngWig')
         })

         .state('app.iasnTrnForwardMail', {
             url: '/iasnTrnForwardMail',
             title: 'iasnTrnForwardMail',
             templateUrl: helper.basepath('ems.iasn/iasnTrnForwardMail.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives', 'ngWig')
         })


         .state('app.MstCibilSummary', {
             url: '/MstCibilSummary',
             title: 'MstCibilSummary',
             templateUrl: helper.basepath('ems.master/MstCibilSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives', 'ngWig', 'filestyle')
         })

         .state('app.MstCibilDataSummary', {
             url: '/MstCibilDataSummary',
             title: 'MstCibilDataSummary',
             templateUrl: helper.basepath('ems.master/MstCibilDataSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives', 'ngWig', 'filestyle')
         })
        .state('app.MstCibilEdit', {
            url: '/MstCibilEdit',
            title: 'MstCibilEdit',
            templateUrl: helper.basepath('ems.master/MstCibilEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives', 'ngWig', 'filestyle')
        })

        .state('app.MstCibilDataLogDetails', {
            url: '/MstCibilDataLogDetails',
            title: 'MstCibilDataLogDetails',
            templateUrl: helper.basepath('ems.master/MstCibilDataLogDetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives', 'ngWig', 'filestyle')
        })

         .state('app.DtsRptUserReport2', {
             url: '/DtsRptUserReport2',
             title: 'DtsRptUserReport2',
             templateUrl: helper.basepath('ems.ecms/DtsRptUserReport2.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })
         .state('app.iasnTrnMyWorkItemPushback', {
             url: '/iasnTrnMyWorkItemPushback',
             title: 'iasnTrnMyWorkItemPushback',
             templateUrl: helper.basepath('ems.iasn/iasnTrnMyWorkItemPushback.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
         })
        .state('app.iasnTrnMyWorkItemForward', {
            url: '/iasnTrnMyWorkItemForward',
            title: 'iasnTrnMyWorkItemForward',
            templateUrl: helper.basepath('ems.iasn/iasnTrnMyWorkItemForward.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })
        .state('app.iasnTrnMyWorkItemClose', {
            url: '/iasnTrnMyWorkItemClose',
            title: 'iasnTrnMyWorkItemClose',
            templateUrl: helper.basepath('ems.iasn/iasnTrnMyWorkItemClose.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })
        .state('app.MstDocumentUploadSummary', {
            url: '/MstDocumentUploadSummary',
            title: 'MstDocumentUploadSummary',
            templateUrl: helper.basepath('ems.master/MstDocumentUploadSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid', 'filestyle', 'ngWig')
        })
        .state('app.MstDocumentDownload', {
            url: '/MstDocumentDownload',
            title: 'MstDocumentDownload',
            templateUrl: helper.basepath('ems.master/MstDocumentDownload.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })

           .state('app.sdcMstModuleSummary',
        {
            url: '/sdcMstModuleSummary',
            title: 'sdcMstModuleSummary',
            templateUrl: helper.basepath('ems.sdc/sdcMstModuleSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

         .state('app.sdcMstTagCustomer',
        {
            url: '/sdcMstTagCustomer',
            title: 'sdcMstTagCustomer',
            templateUrl: helper.basepath('ems.sdc/sdcMstTagCustomer.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

          .state('app.sdcTrnDeploymentSummary',
        {
            url: '/sdcTrnDeploymentSummary',
            title: 'sdcTrnDeploymentSummary',
            templateUrl: helper.basepath('ems.sdc/sdcTrnDeploymentSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

            .state('app.sdcTrnAddDeployment',
        {
            url: '/sdcTrnAddDeployment',
            title: 'sdcTrnAddDeployment',
            templateUrl: helper.basepath('ems.sdc/sdcTrnAddDeployment.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
            .state('app.sdcTrnTestDeploymentSummary',
        {
            url: '/sdcTrnTestDeploymentSummary',
            title: 'sdcTrnTestDeploymentSummary',
            templateUrl: helper.basepath('ems.sdc/sdcTrnTestDeploymentSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
           .state('app.LglTrnDNTrackerHistory',
        {
            url: '/LglTrnDNTrackerHistory',
            title: 'LglTrnDNTrackerHistory',
            templateUrl: helper.basepath('ems.lgl/LglTrnDNTrackerHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.osdCqmQueryAssignment',
        {
            url: '/osdCqmQueryAssignment',
            title: 'osdCqmQueryAssignment',
            templateUrl: helper.basepath('ems.osd/osdCqmQueryAssignment.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.osdCqmQueryTicketAssignment',
        {
            url: '/osdCqmQueryTicketAssignment',
            title: 'osdCqmQueryTicketAssignment',
            templateUrl: helper.basepath('ems.osd/osdCqmQueryTicketAssignment.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.osdCqmAssignToQuery',
        {
            url: '/osdCqmAssignToQuery',
            title: 'osdCqmAssignToQuery',
            templateUrl: helper.basepath('ems.osd/osdCqmAssignToQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.osdCqmQueryAssign360',
        {
            url: '/osdCqmQueryAssign360',
            title: 'osdCqmQueryAssign360',
            templateUrl: helper.basepath('ems.osd/osdCqmQueryAssign360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.osdCqmAssignedQuery360',
        {
            url: '/osdCqmAssignedQuery360',
            title: 'osdCqmAssignedQuery360',
            templateUrl: helper.basepath('ems.osd/osdCqmAssignedQuery360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.osdCqmAssignedQuery',
        {
            url: '/osdCqmAssignedQuery',
            title: 'osdCqmAssignedQuery',
            templateUrl: helper.basepath('ems.osd/osdCqmAssignedQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.osdComposeMail', {
            url: '/osdComposeMail',
            title: 'osdComposeMail',
            templateUrl: helper.basepath('ems.osd/osdComposeMail.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.osdCqmCloseView', {
            url: '/osdCqmCloseView',
            title: 'osdCqmCloseView',
            templateUrl: helper.basepath('ems.osd/osdCqmCloseView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.osdCqmCloseSummary', {
            url: '/osdCqmCloseSummary',
            title: 'osdCqmCloseSummary',
            templateUrl: helper.basepath('ems.osd/osdCqmCloseSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig')
        })
        .state('app.osdCqmAssignCloseSummary', {
            url: '/osdCqmAssignCloseSummary',
            title: 'osdCqmAssignCloseSummary',
            templateUrl: helper.basepath('ems.osd/osdCqmAssignCloseSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig')
        })
        .state('app.osdCqmAssignCloseView', {
            url: '/osdCqmAssignCloseView',
            title: 'osdCqmAssignCloseView',
            templateUrl: helper.basepath('ems.osd/osdCqmAssignCloseView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.osdCqmReplaySummary', {
            url: '/osdCqmReplaySummary',
            title: 'osdCqmReplaySummary',
            templateUrl: helper.basepath('ems.osd/osdCqmReplaySummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig')
        })
        .state('app.osdCqmReplayView ', {
            url: '/osdCqmReplayView',
            title: 'osdCqmReplayView',
            templateUrl: helper.basepath('ems.osd/osdCqmReplayView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.osdCqmForwardSummary', {
            url: '/osdCqmForwardSummary',
            title: 'osdCqmForwardSummary',
            templateUrl: helper.basepath('ems.osd/osdCqmForwardSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig')
        })
        .state('app.osdCqmForwardView ', {
            url: '/osdCqmForwardView',
            title: 'osdCqmForwardView',
            templateUrl: helper.basepath('ems.osd/osdCqmForwardView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.osdCqmTransferSummary', {
            url: '/osdCqmTransferSummary',
            title: 'osdCqmTransferSummary',
            templateUrl: helper.basepath('ems.osd/osdCqmTransferSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig')
        })
        .state('app.osdCqmTransferView', {
            url: '/osdCqmTransferView',
            title: 'osdCqmTransferView',
            templateUrl: helper.basepath('ems.osd/osdCqmTransferView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
            .state('app.MstRMmappingSummary',
        {
            url: '/MstRMmappingSummary',
            title: 'MstRMmappingSummary',
            templateUrl: helper.basepath('ems.master/MstRMmappingSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
           .state('app.MstRMCustomerSummary',
        {
            url: '/MstRMCustomerSummary',
            title: 'MstRMCustomerSummary',
            templateUrl: helper.basepath('ems.master/MstRMCustomerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
          .state('app.MstRMCustomerView',
        {
            url: '/MstRMCustomerView',
            title: 'MstRMCustomerView',
            templateUrl: helper.basepath('ems.master/MstRMCustomerView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.MstRMCustomer2userView',
        {
            url: '/MstRMCustomer2userView',
            title: 'MstRMCustomer2userView',
            templateUrl: helper.basepath('ems.master/MstRMCustomer2userView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.osdBamAllocatedToRM', {
            url: '/osdBamAllocatedToRM',
            title: 'osdBamAllocatedToRM',
            templateUrl: helper.basepath('ems.osd/osdBamAllocatedToRM.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.osdBamAllocatedToRMView', {
            url: '/osdBamAllocatedToRMView',
            title: 'osdBamAllocatedToRMView',
            templateUrl: helper.basepath('ems.osd/osdBamAllocatedToRMView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

           .state('app.OsdTrnBankAlertManagementSummary', {
               url: '/OsdTrnBankAlertManagementSummary',
               title: 'OsdTrnBankAlertManagementSummary',
               templateUrl: helper.basepath('ems.osd/OsdTrnBankAlertManagementSummary.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })

         .state('app.osdBamAssign2operation', {
             url: '/osdBamAssign2operation',
             title: 'osdBamAssign2operation',
             templateUrl: helper.basepath('ems.osd/osdBamAssign2operation.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.osdBamNotAllocatedView', {
             url: '/osdBamNotAllocatedView',
             title: 'osdBamNotAllocatedView',
             templateUrl: helper.basepath('ems.osd/osdBamNotAllocatedView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.osdBamAllocatedView', {
             url: '/osdBamAllocatedView',
             title: 'osdBamAllocatedView',
             templateUrl: helper.basepath('ems.osd/osdBamAllocatedView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
         .state('app.osdBAMrmtransfer', {
             url: '/osdBAMrmtransfer',
             title: 'osdBAMrmtransfer',
             templateUrl: helper.basepath('ems.osd/osdBAMrmtransfer.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
         .state('app.MstReligion', {
             url: '/MstReligion',
             title: 'MstReligion',
             templateUrl: helper.basepath('ems.master/MstReligion.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
         })
        .state('app.MstEducation',
        {
            url: '/MstEducation',
            title: 'MstEducation',
            templateUrl: helper.basepath('ems.master/MstEducation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstCompanyType',
        {
            url: '/MstCompanyType',
            title: 'MstCompanyType',
            templateUrl: helper.basepath('ems.master/MstCompanyType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstCreditTypeSummary',
        {
            url: '/MstCreditTypeSummary',
            title: 'MstCreditTypeSummary',
            templateUrl: helper.basepath('ems.master/MstCreditTypeSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstLoanType',
        {
            url: '/MstLoanType',
            title: 'MstLoanType',
            templateUrl: helper.basepath('ems.master/MstLoanType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstCountryCode',
        {
            url: '/MstCountryCode',
            title: 'MstCountryCode',
            templateUrl: helper.basepath('ems.master/MstCountryCode.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstAssessmentAgency',
        {
            url: '/MstAssessmentAgency',
            title: 'MstAssessmentAgency',
            templateUrl: helper.basepath('ems.master/MstAssessmentAgency.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstInterestFrequency',
        {
            url: '/MstInterestFrequency',
            title: 'MstInterestFrequency',
            templateUrl: helper.basepath('ems.master/MstInterestFrequency.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstVerticalTags',
        {
            url: '/MstVerticalTags',
            title: 'MstVerticalTags',
            templateUrl: helper.basepath('ems.master/MstVerticalTags.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstPayment',
        {
            url: '/MstPayment',
            title: 'MstPayment',
            templateUrl: helper.basepath('ems.master/MstPayment.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstAreaType',
        {
            url: '/MstAreaType',
            title: 'MstAreaType',
            templateUrl: helper.basepath('ems.master/MstAreaType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstSAType',
        {
            url: '/MstSAType',
            title: 'MstSAType',
            templateUrl: helper.basepath('ems.master/MstSAType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstSAEntityType',
        {
            url: '/MstSAEntityType',
            title: 'MstSAType',
            templateUrl: helper.basepath('ems.master/MstSAEntityType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstSADocumentList',
        {
            url: '/MstSADocumentList',
            title: 'MstSADocumentList',
            templateUrl: helper.basepath('ems.master/MstSADocumentList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstVernacularLanguage',
        {
            url: '/MstVernacularLanguage',
            title: 'MstVernacularLanguage',
            templateUrl: helper.basepath('ems.master/MstVernacularLanguage.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
            .state('app.MstLenderType',
        {
            url: '/MstLenderType',
            title: 'MstLenderType',
            templateUrl: helper.basepath('ems.master/MstLenderType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
             .state('app.MstCreditUnderwritingFacilityType',
        {
            url: '/MstCreditUnderwritingFacilityType',
            title: 'MstCreditUnderwritingFacilityType',
            templateUrl: helper.basepath('ems.master/MstCreditUnderwritingFacilityType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
             .state('app.MstGenderSummary',
        {
            url: '/MstGenderSummary',
            title: 'MstGenderSummary',
            templateUrl: helper.basepath('ems.master/MstGenderSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
              .state('app.MstMaritalStatusSummary',
        {
            url: '/MstMaritalStatusSummary',
            title: 'MstMaritalStatusSummary',
            templateUrl: helper.basepath('ems.master/MstMaritalStatusSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
            .state('app.MstLoanProductsSummary',
        {
            url: '/MstLoanProductsSummary',
            title: 'MstLoanProductsSummary',
            templateUrl: helper.basepath('ems.master/MstLoanProductsSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstLoanTermPeriodSummary',
        {
            url: '/MstLoanTermPeriodSummary',
            title: 'MstLoanTermPeriodSummary',
            templateUrl: helper.basepath('ems.master/MstLoanTermPeriodSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
            .state('app.MstAmortizationMethodSummary',
        {
            url: '/MstAmortizationMethodSummary',
            title: 'MstAmortizationMethodSummary',
            templateUrl: helper.basepath('ems.master/MstAmortizationMethodSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstFundedTypeIndicatorSummary',
        {
            url: '/MstFundedTypeIndicatorSummary',
            title: 'MstFundedTypeIndicatorSummary',
            templateUrl: helper.basepath('ems.master/MstFundedTypeIndicatorSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstTypeofDebtSummary',
        {
            url: '/MstTypeofDebtSummary',
            title: 'MstTypeofDebtSummary',
            templateUrl: helper.basepath('ems.master/MstTypeofDebtSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstLoanPurpose',
        {
            url: '/MstLoanPurpose',
            title: 'MstLoanPurpose',
            templateUrl: helper.basepath('ems.master/MstLoanPurpose.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstAssessmentAgencyRating',
        {
            url: '/MstAssessmentAgencyRating',
            title: 'MstAssessmentAgencyRating',
            templateUrl: helper.basepath('ems.master/MstAssessmentAgencyRating.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstCreditTypeOfFacility', {
            url: '/MstCreditTypeOfFacility',
            title: 'MstCreditTypeOfFacility',
            templateUrl: helper.basepath('ems.master/MstCreditTypeOfFacility.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
       .state('app.MstCreditAccountClassification', {
           url: '/MstCreditAccountClassification',
           title: 'MstCreditAccountClassification',
           templateUrl: helper.basepath('ems.master/MstCreditAccountClassification.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
       })
       .state('app.MstCreditInstalmentFrequency', {
           url: '/MstCreditInstalmentFrequency',
           title: 'MstCreditInstalmentFrequency',
           templateUrl: helper.basepath('ems.master/MstCreditInstalmentFrequency.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
       })
       .state('app.MstCreditTypeOfExistingFunded', {
           url: '/MstCreditTypeOfExistingFunded',
           title: 'MstCreditTypeOfExistingFunded',
           templateUrl: helper.basepath('ems.master/MstCreditTypeOfExistingFunded.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
       })
       .state('app.MstIndividualDocument', {
           url: '/MstIndividualDocument',
           title: 'MstIndividualDocument',
           templateUrl: helper.basepath('ems.master/MstIndividualDocument.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
       })
    .state('app.MstCompanyDocument', {
        url: '/MstCompanyDocument',
        title: 'MstCompanyDocument',
        templateUrl: helper.basepath('ems.master/MstCompanyDocument.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstBureauName', {
        url: '/MstBureauName',
        title: 'MstBureauName',
        templateUrl: helper.basepath('ems.master/MstBureauName.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstAMLCategory', {
        url: '/MstAMLCategory',
        title: 'MstAMLCategory',
        templateUrl: helper.basepath('ems.master/MstAMLCategory.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstSecurityClassification', {
        url: '/MstSecurityClassification',
        title: 'MstSecurityClassification',
        templateUrl: helper.basepath('ems.master/MstSecurityClassification.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstSecurityCoverage', {
        url: '/MstSecurityCoverage',
        title: 'MstSecurityCoverage',
        templateUrl: helper.basepath('ems.master/MstSecurityCoverage.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstGuaranteeCoverage', {
        url: '/MstGuaranteeCoverage',
        title: 'MstGuaranteeCoverage',
        templateUrl: helper.basepath('ems.master/MstGuaranteeCoverage.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstBusinessCategory', {
        url: '/MstBusinessCategory',
        title: 'MstBusinessCategory',
        templateUrl: helper.basepath('ems.master/MstBusinessCategory.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstBusinessIndustryType', {
        url: '/MstBusinessIndustryType',
        title: 'MstBusinessIndustryType',
        templateUrl: helper.basepath('ems.master/MstBusinessIndustryType.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstBankAccountLevel', {
        url: '/MstBankAccountLevel',
        title: 'MstBankAccountLevel',
        templateUrl: helper.basepath('ems.master/MstBankAccountLevel.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstRelationship', {
        url: '/MstRelationship',
        title: 'MstRelationship',
        templateUrl: helper.basepath('ems.master/MstRelationship.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstSamunnatiBranchName', {
        url: '/MstSamunnatiBranchName',
        title: 'MstSamunnatiBranchName',
        templateUrl: helper.basepath('ems.master/MstSamunnatiBranchName.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstSamunnatiBranchState', {
        url: '/MstSamunnatiBranchState',
        title: 'MstSamunnatiBranchState',
        templateUrl: helper.basepath('ems.master/MstSamunnatiBranchState.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstGeneticCode', {
        url: '/MstGeneticCode',
        title: 'MstGeneticCode',
        templateUrl: helper.basepath('ems.master/MstGeneticCode.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstEntity', {
        url: '/MstEntity',
        title: 'MstEntity',
        templateUrl: helper.basepath('ems.master/MstEntity.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstOwnerShipType', {
        url: '/MstOwnerShipType',
        title: 'MstOwnerShipType',
        templateUrl: helper.basepath('ems.master/MstOwnerShipType.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstResidenceType', {
        url: '/MstResidenceType',
        title: 'MstResidenceType',
        templateUrl: helper.basepath('ems.master/MstResidenceType.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstLicenseType', {
        url: '/MstLicenseType',
        title: 'MstLicenseType',
        templateUrl: helper.basepath('ems.master/MstLicenseType.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstConstructionType', {
        url: '/MstConstructionType',
        title: 'MstConstructionType',
        templateUrl: helper.basepath('ems.master/MstConstructionType.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstCaste', {
        url: '/MstCaste',
        title: 'MstCaste',
        templateUrl: helper.basepath('ems.master/MstCaste.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstPartyType', {
        url: '/MstPartyType',
        title: 'MstPartyType',
        templateUrl: helper.basepath('ems.master/MstPartyType.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstTypeofChargecreated', {
        url: '/MstTypeofChargecreated',
        title: 'MstTypeofChargecreated',
        templateUrl: helper.basepath('ems.master/MstTypeofChargecreated.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstAssetstype', {
        url: '/MstAssetstype',
        title: 'MstAssetstype',
        templateUrl: helper.basepath('ems.master/MstAssetstype.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstLendingarrangement', {
        url: '/MstLendingarrangement',
        title: 'MstLendingarrangement',
        templateUrl: helper.basepath('ems.master/MstLendingarrangement.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstIncomeType', {
        url: '/MstIncomeType',
        title: 'MstIncomeType',
        templateUrl: helper.basepath('ems.master/MstIncomeType.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })
    .state('app.MstIndividualProof', {
        url: '/MstIndividualProof',
        title: 'MstIndividualProof',
        templateUrl: helper.basepath('ems.master/MstIndividualProof.html?ver=' + version + '"'),
        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
    })

    .state('app.MstCreditType',
        {
            url: '/MstCreditType',
            title: 'MstCreditType',
            templateUrl: helper.basepath('ems.master/MstCreditType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstBuyerSummary', {
            url: '/MstBuyerSummary',
            title: 'MstBuyerSummary',
            templateUrl: helper.basepath('ems.master/MstBuyerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstBuyerAdd', {
            url: '/MstBuyerAdd',
            title: 'MstBuyerAdd',
            templateUrl: helper.basepath('ems.master/MstBuyerAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
             .state('app.MstBuyerEdit', {
                 url: '/MstBuyerEdit',
                 title: 'MstBuyerEdit',
                 templateUrl: helper.basepath('ems.master/MstBuyerEdit.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
	 .state('app.MstBuyerView', {
	     url: '/MstBuyerView',
	     title: 'MstBuyerView',
	     templateUrl: helper.basepath('ems.master/MstBuyerView.html?ver=' + version + '"'),
	     resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
	 })
        .state('app.MstCreditStatusSummary', {
            url: '/MstCreditStatusSummary',
            title: 'MstCreditStatusSummary',
            templateUrl: helper.basepath('ems.master/MstCreditStatusSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditStatusAdd', {
            url: '/MstCreditStatusAdd',
            title: 'MstCreditStatusAdd',
            templateUrl: helper.basepath('ems.master/MstCreditStatusAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })
        .state('app.MstPrincipalFrequency',
        {
            url: '/MstPrincipalFrequency',
            title: 'MstPrincipalFrequency',
            templateUrl: helper.basepath('ems.master/MstPrincipalFrequency.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstGender',
        {
            url: '/MstGender',
            title: 'MstGender',
            templateUrl: helper.basepath('ems.master/MstGender.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstMaritalStatus',
        {
            url: '/MstMaritalStatus',
            title: 'MstMaritalStatus',
            templateUrl: helper.basepath('ems.master/MstMaritalStatus.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstFundedTypeIndicator',
        {
            url: '/MstFundedTypeIndicator',
            title: 'MstFundedTypeIndicator',
            templateUrl: helper.basepath('ems.master/MstFundedTypeIndicator.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })

          .state('app.MstLoanSubProduct',
        {
            url: '/MstLoanSubProduct',
            title: 'MstLoanSubProduct',
            templateUrl: helper.basepath('ems.master/MstLoanSubProduct.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })

          .state('app.MstColending',
        {
            url: '/MstColending',
            title: 'MstColending',
            templateUrl: helper.basepath('ems.master/MstColending.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })
        .state('app.MstApplicationCreationSummary', {
            url: '/MstApplicationCreationSummary',
            title: 'MstApplicationCreationSummary',
            templateUrl: helper.basepath('ems.master/MstApplicationCreationSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstApplicationGeneralAdd', {
            url: '/MstApplicationGeneralAdd',
            title: 'MstApplicationGeneralAdd',
            templateUrl: helper.basepath('ems.master/MstApplicationGeneralAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })
        .state('app.MstGradingToolAdd', {
            url: '/MstGradingToolAdd',
            title: 'MstGradingToolAdd',
            templateUrl: helper.basepath('ems.master/MstGradingToolAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })

        .state('app.MstBuyer2CreditStatusView', {
            url: '/MstBuyer2CreditStatusView',
            title: 'MstBuyer2CreditStatusView',
            templateUrl: helper.basepath('ems.master/MstBuyer2CreditStatusView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })

        .state('app.MstVisitReportAdd', {
            url: '/MstVisitReportAdd',
            title: 'MstVisitReportAdd',
            templateUrl: helper.basepath('ems.master/MstVisitReportAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })

                    .state('app.idasMstTemplateSummary',
        {
            url: '/idasMstTemplateSummary',
            title: 'idasMstTemplateSummary',
            templateUrl: helper.basepath('ems.idas/idasMstTemplateSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })

             .state('app.idasMstAddTemplate',
        {
            url: '/idasMstAddTemplate',
            title: 'idasMstAddTemplate',
            templateUrl: helper.basepath('ems.idas/idasMstAddTemplate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })

               .state('app.viewTemplateDetails',
        {
            url: '/viewTemplateDetails',
            title: 'viewTemplateDetails',
            templateUrl: helper.basepath('ems.idas/viewTemplateDetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })

                .state('app.idasMstEditTemplate',
        {
            url: '/idasMstEditTemplate',
            title: 'idasMstEditTemplate',
            templateUrl: helper.basepath('ems.idas/idasMstEditTemplate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })

             .state('app.idasMstSanctionLetterGeneration',
        {
            url: '/idasMstSanctionLetterGeneration',
            title: 'idasMstSanctionLetterGeneration',
            templateUrl: helper.basepath('ems.idas/idasMstSanctionLetterGeneration.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'textAngular', 'ngWig')

        })

         .state('app.MstCreditStatusApprovedBuyer', {
             url: '/MstCreditStatusApprovedBuyer',
             title: 'MstCreditStatusApprovedBuyer',
             templateUrl: helper.basepath('ems.master/MstCreditStatusApprovedBuyer.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })


         .state('app.MstCreditStatusNonApprovedBuyer', {
             url: '/MstCreditStatusNonApprovedBuyer',
             title: 'MstCreditStatusNonApprovedBuyer',
             templateUrl: helper.basepath('ems.master/MstCreditStatusNonApprovedBuyer.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.MstCreditPolicyCompliance', {
             url: '/MstCreditPolicyCompliance',
             title: 'MstCreditPolicyCompliance',
             templateUrl: helper.basepath('ems.master/MstCreditPolicyCompliance.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
        .state('app.MstApplicationGeneralEdit', {
            url: '/MstApplicationGeneralEdit',
            title: 'MstApplicationGeneralEdit',
            templateUrl: helper.basepath('ems.master/MstApplicationGeneralEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })

        .state('app.MstApplcreationBasicdtlEdit', {
            url: '/MstApplcreationBasicdtlEdit',
            title: 'MstApplcreationBasicdtlEdit',
            templateUrl: helper.basepath('ems.master/MstApplcreationBasicdtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
	        .state('app.MstApplcreationIndividualdtlEdit', {
	            url: '/MstApplcreationIndividualdtlEdit',
	            title: 'MstApplcreationIndividualdtlEdit',
	            templateUrl: helper.basepath('ems.master/MstApplcreationIndividualdtlEdit.html?ver=' + version + '"'),
	            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
	        })
        .state('app.MstApplcreationInstitutiondtlEdit', {
            url: '/MstApplcreationInstitutiondtlEdit',
            title: 'MstApplcreationInstitutiondtlEdit',
            templateUrl: helper.basepath('ems.master/MstApplcreationInstitutiondtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

            .state('app.MstApplcreationGroupdtlEdit', {
                url: '/MstApplcreationGroupdtlEdit',
                title: 'MstApplcreationGroupdtlEdit',
                templateUrl: helper.basepath('ems.master/MstApplcreationGroupdtlEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })

        .state('app.MstApplcreationSocialTradeEdit', {
            url: '/MstApplcreationSocialTradeEdit',
            title: 'MstApplcreationSocialTradeEdit',
            templateUrl: helper.basepath('ems.master/MstApplcreationSocialTradeEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })
        .state('app.MstApplcreationProductchargesEdit', {
            url: '/MstApplcreationProductchargesEdit',
            title: 'MstApplcreationProductchargesEdit',
            templateUrl: helper.basepath('ems.master/MstApplcreationProductchargesEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })
        .state('app.MstApplcreationCICUploadEdit', {
            url: '/MstApplcreationCICUploadEdit',
            title: 'MstApplcreationCICUploadEdit',
            templateUrl: helper.basepath('ems.master/MstApplcreationCICUploadEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })
        .state('app.MstSAOnboardingSummary', {
            url: '/MstSAOnboardingSummary',
            title: 'MstSAOnboardingSummary',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAOnboardingAddInstitution', {
            url: '/MstSAOnboardingAddInstitution',
            title: 'MstSAOnboardingAddInstitution',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingAddInstitution.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstSAOnboardingAddIndividual', {
            url: '/MstSAOnboardingAddIndividual',
            title: 'MstSAOnboardingAddIndividual',
            templateUrl: helper.basepath('ems.master/MstSAOnboardingAddIndividual.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstaddSAOnboarding', {
            url: '/MstaddSAOnboarding',
            title: 'MstaddSAOnboarding',
            templateUrl: helper.basepath('ems.master/MstaddSAOnboarding.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstApplicationCreationView', {
            url: '/MstApplicationCreationView',
            title: 'MstApplicationCreationView',
            templateUrl: helper.basepath('ems.master/MstApplicationCreationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })
        .state('app.MstApplCreationIndividualGuarantorView', {
            url: '/MstApplCreationIndividualGuarantorView',
            title: 'MstApplCreationIndividualGuarantorView',
            templateUrl: helper.basepath('ems.master/MstApplCreationIndividualGuarantorView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })
        .state('app.MstApplCreationInstitutionGuarantorView', {
            url: '/MstApplCreationInstitutionGuarantorView',
            title: 'MstApplCreationInstitutionGuarantorView',
            templateUrl: helper.basepath('ems.master/MstApplCreationInstitutionGuarantorView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })
        .state('app.MstApplCreationGradingToolView', {
            url: '/MstApplCreationGradingToolView',
            title: 'MstApplCreationGradingToolView',
            templateUrl: helper.basepath('ems.master/MstApplCreationGradingToolView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })
        .state('app.MstApplCreationVisitReportView', {
            url: '/MstApplCreationVisitReportView',
            title: 'MstApplCreationVisitReportView',
            templateUrl: helper.basepath('ems.master/MstApplCreationVisitReportView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })

        .state('app.MstApplcreationCICUploadInstEdit', {
            url: '/MstApplcreationCICUploadInstEdit',
            title: 'MstApplcreationCICUploadInstEdit',
            templateUrl: helper.basepath('ems.master/MstApplcreationCICUploadInstEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstAssessmentCriteria', {
            url: '/MstAssessmentCriteria',
            title: 'MstAssessmentCriteria',
            templateUrl: helper.basepath('ems.master/MstAssessmentCriteria.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
         .state('app.MstApplicationAssignmentSummary', {
             url: '/MstApplicationAssignmentSummary',
             title: 'MstApplicationAssignmentSummary',
             templateUrl: helper.basepath('ems.master/MstApplicationAssignmentSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

        .state('app.MstAppassignedAssignmentSummary', {
            url: '/MstAppassignedAssignmentSummary',
            title: 'MstAppassignedAssignmentSummary',
            templateUrl: helper.basepath('ems.master/MstAppassignedAssignmentSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
         .state('app.MstVisitReportEdit', {
             url: '/MstVisitReportEdit',
             title: 'MstVisitReportEdit',
             templateUrl: helper.basepath('ems.master/MstVisitReportEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')



         })
                .state('app.MstVisitReportView', {
                    url: '/MstVisitReportView',
                    title: 'MstVisitReportView',
                    templateUrl: helper.basepath('ems.master/MstVisitReportView.html?ver=' + version + '"'),
                    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')



                })

         .state('app.idasTrnDocumentTaggingCreditChild',
        {
            url: '/idasTrnDocumentTaggingCreditChild',
            title: 'idasTrnDocumentTaggingCreditChild',
            templateUrl: helper.basepath('ems.idas/idasTrnDocumentTaggingCreditChild.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

         .state('app.MstAssessmentCriteriaDetailsEdit', {
             url: '/MstAssessmentCriteriaDetailsEdit',
             title: 'MstAssessmentCriteriaDetailsEdit',
             templateUrl: helper.basepath('ems.master/MstAssessmentCriteriaDetailsEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

         })
            .state('app.MstAssessmentCriteriaDetailsView', {
                url: '/MstAssessmentCriteriaDetailsView',
                title: 'MstAssessmentCriteriaDetailsView',
                templateUrl: helper.basepath('ems.master/MstAssessmentCriteriaDetailsView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

            })

         .state('app.MstGradingToolEdit', {
             url: '/MstGradingToolEdit',
             title: 'MstGradingToolEdit',
             templateUrl: helper.basepath('ems.master/MstGradingToolEdit.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

         })
         .state('app.MstGradingToolView', {
             url: '/MstGradingToolView',
             title: 'MstGradingToolView',
             templateUrl: helper.basepath('ems.master/MstGradingToolView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

         })
             .state('app.MstApplicationInstitutionAdd', {
                 url: '/MstApplicationInstitutionAdd',
                 title: 'MstApplicationInstitutionAdd',
                 templateUrl: helper.basepath('ems.master/MstApplicationInstitutionAdd.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })
            .state('app.MstApplicationOverallLimitAdd', {
                url: '/MstApplicationOverallLimitAdd',
                title: 'MstApplicationOverallLimitAdd',
                templateUrl: helper.basepath('ems.master/MstApplicationOverallLimitAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstApplicationProductChargesAdd', {
                url: '/MstApplicationProductChargesAdd',
                title: 'MstApplicationProductChargesAdd',
                templateUrl: helper.basepath('ems.master/MstApplicationProductChargesAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstApplicationServiceChargeAdd', {
                url: '/MstApplicationServiceChargeAdd',
                title: 'MstApplicationServiceChargeAdd',
                templateUrl: helper.basepath('ems.master/MstApplicationServiceChargeAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstApplicationHypothecationAdd', {
                url: '/MstApplicationHypothecationAdd',
                title: 'MstApplicationHypothecationAdd',
                templateUrl: helper.basepath('ems.master/MstApplicationHypothecationAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
            .state('app.MstApplicationIndividualAdd', {
                url: '/MstApplicationIndividualAdd',
                title: 'MstApplicationIndividualAdd',
                templateUrl: helper.basepath('ems.master/MstApplicationIndividualAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

            })

            .state('app.MstApplicationCICUploadAdd', {
                url: '/MstApplicationCICUploadAdd',
                title: 'MstApplicationCICUploadAdd',
                templateUrl: helper.basepath('ems.master/MstApplicationCICUploadAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

            })

            .state('app.MstApplicationSocialTradeCapitalAdd', {
                url: '/MstApplicationSocialTradeCapitalAdd',
                title: 'MstApplicationSocialTradeCapitalAdd',
                templateUrl: helper.basepath('ems.master/MstApplicationSocialTradeCapitalAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

            })

            .state('app.MstApplicationGroupAdd', {
                url: '/MstApplicationGroupAdd',
                title: 'MstApplicationGroupAdd',
                templateUrl: helper.basepath('ems.master/MstApplicationGroupAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

            })

            .state('app.MstApplicationIndividualEdit', {
                url: '/MstApplicationIndividualEdit',
                title: 'MstApplicationIndividualEdit',
                templateUrl: helper.basepath('ems.master/MstApplicationIndividualEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

            })

           .state('app.MstApplicationSocialTradeCapitalEdit', {
               url: '/MstApplicationSocialTradeCapitalEdit',
               title: 'MstApplicationSocialTradeCapitalEdit',
               templateUrl: helper.basepath('ems.master/MstApplicationSocialTradeCapitalEdit.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

           })

            .state('app.MstApplicationProductChargesEdit', {
                url: '/MstApplicationProductChargesEdit',
                title: 'MstApplicationProductChargesEdit',
                templateUrl: helper.basepath('ems.master/MstApplicationProductChargesEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

            })

            .state('app.MstApplicationInstitutionEdit', {
                url: '/MstApplicationInstitutionEdit',
                title: 'MstApplicationInstitutionEdit',
                templateUrl: helper.basepath('ems.master/MstApplicationInstitutionEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

            })
            .state('app.MstApplicationGroupEdit', {
                url: '/MstApplicationGroupEdit',
                title: 'MstApplicationGroupEdit',
                templateUrl: helper.basepath('ems.master/MstApplicationGroupEdit.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

            })

          .state('app.idasTrnDocumentTaggingCreditOperationsChild', {
              url: '/idasTrnDocumentTaggingCreditOperationsChild',
              title: 'idasTrnDocumentTaggingCreditOperationsChild',
              templateUrl: helper.basepath('ems.idas/idasTrnDocumentTaggingCreditOperationsChild.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

          })
        .state('app.MstApplicationHypothecationEdit', {
            url: '/MstApplicationHypothecationEdit',
            title: 'MstApplicationHypothecationEdit',
            templateUrl: helper.basepath('ems.master/MstApplicationHypothecationEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })

             .state('app.MstApplicationEditKycView', {
                 url: '/MstApplicationEditKycView',
                 title: 'MstApplicationEditKycView',
                 templateUrl: helper.basepath('ems.master/MstApplicationEditKycView.html?ver=' + version + '"'),
                 resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
             })

            .state('app.MstApplGroupMemberdtlView', {
                url: '/MstApplGroupMemberdtlView',
                title: 'MstApplGroupMemberdtlView',
                templateUrl: helper.basepath('ems.master/MstApplGroupMemberdtlView.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

            })

         .state('app.MstAppEditHypothecationAdd', {
             url: '/MstAppEditHypothecationAdd',
             title: 'MstAppEditHypothecationAdd',
             templateUrl: helper.basepath('ems.master/MstAppEditHypothecationAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

         })

         .state('app.MstAppEditOverallLimitAdd', {
             url: '/MstAppEditOverallLimitAdd',
             title: 'MstAppEditOverallLimitAdd',
             templateUrl: helper.basepath('ems.master/MstAppEditOverallLimitAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

         })
        .state('app.MstAppEditProductAdd', {
            url: '/MstAppEditProductAdd',
            title: 'MstAppEditProductAdd',
            templateUrl: helper.basepath('ems.master/MstAppEditProductAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })

         .state('app.MstAppEditChargeAdd', {
             url: '/MstAppEditChargeAdd',
             title: 'MstAppEditChargeAdd',
             templateUrl: helper.basepath('ems.master/MstAppEditChargeAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

         })

        .state('app.MstApplicationLoanEdit', {
            url: '/MstApplicationLoanEdit',
            title: 'MstApplicationLoanEdit',
            templateUrl: helper.basepath('ems.master/MstApplicationLoanEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })


          .state('app.osdTrnTicketAllotedSummary',
        {
            url: '/osdTrnTicketAllotedSummary',
            title: 'osdTrnTicketAllotedSummary',
            templateUrl: helper.basepath('ems.osd/osdTrnTicketAllotedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })


         .state('app.osdTrnTicketWorkSummary',
        {
            url: '/osdTrnTicketWorkSummary',
            title: 'osdTrnTicketWorkSummary',
            templateUrl: helper.basepath('ems.osd/osdTrnTicketWorkSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

           .state('app.osdTrnTicketForwardSummary',
        {
            url: '/osdTrnTicketForwardSummary',
            title: 'osdTrnTicketForwardSummary',
            templateUrl: helper.basepath('ems.osd/osdTrnTicketForwardSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

         .state('app.osdTrnTicketCompletedSummary',
        {
            url: '/osdTrnTicketCompletedSummary',
            title: 'osdTrnTicketCompletedSummary',
            templateUrl: helper.basepath('ems.osd/osdTrnTicketCompletedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

         .state('app.osdTrnTicketClosedSummary',
        {
            url: '/osdTrnTicketClosedSummary',
            title: 'osdTrnTicketClosedSummary',
            templateUrl: helper.basepath('ems.osd/osdTrnTicketClosedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

         .state('app.MstPslCategory',
        {
            url: '/MstPslCategory',
            title: 'MstPslCategory',
            templateUrl: helper.basepath('ems.master/MstPslCategory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
         .state('app.MstWeakerSection',
        {
            url: '/MstWeakerSection',
            title: 'MstWeakerSection',
            templateUrl: helper.basepath('ems.master/MstWeakerSection.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
         .state('app.MstPslPurpose',
        {
            url: '/MstPslPurpose',
            title: 'MstPslPurpose',
            templateUrl: helper.basepath('ems.master/MstPslPurpose.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
 	     .state('app.MstLineofActivity',
        {
            url: '/MstLineofActivity',
            title: 'MstLineofActivity',
            templateUrl: helper.basepath('ems.master/MstLineofActivity.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
         .state('app.MstBSRCode',
        {
            url: '/MstBSRCode',
            title: 'MstBSRCode',
            templateUrl: helper.basepath('ems.master/MstBSRCode.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
         .state('app.MstOccupation',
        {
            url: '/MstOccupation',
            title: 'MstOccupation',
            templateUrl: helper.basepath('ems.master/MstOccupation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
         .state('app.MstTurnover',
        {
            url: '/MstTurnover',
            title: 'MstTurnover',
            templateUrl: helper.basepath('ems.master/MstTurnover.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
         .state('app.MstInvestment',
        {
            url: '/MstInvestment',
            title: 'MstInvestment',
            templateUrl: helper.basepath('ems.master/MstInvestment.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
         .state('app.MstPurposecolumn',
        {
            url: '/MstPurposecolumn',
            title: 'MstPurposecolumn',
            templateUrl: helper.basepath('ems.master/MstPurposecolumn.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
         .state('app.MstNatureofEntity',
        {
            url: '/MstNatureofEntity',
            title: 'MstNatureofEntity',
            templateUrl: helper.basepath('ems.master/MstNatureofEntity.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
         .state('app.MstMsme',
        {
            url: '/MstMsme',
            title: 'MstMsme',
            templateUrl: helper.basepath('ems.master/MstMsme.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })

        .state('app.MstApplicationEditCICUploadAdd', {
            url: '/MstApplicationEditCICUploadAdd',
            title: 'MstApplicationEditCICUploadAdd',
            templateUrl: helper.basepath('ems.master/MstApplicationEditCICUploadAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')

        })
   	    .state('app.MstBankAccountType',
            {
                url: '/MstBankAccountType',
                title: 'MstBankAccountType',
                templateUrl: helper.basepath('ems.master/MstBankAccountType.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
            })

          .state('app.sdcTrnUatSummary',
        {
            url: '/sdcTrnUatSummary',
            title: 'sdcTrnUatSummary',
            templateUrl: helper.basepath('ems.sdc/sdcTrnUatSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
         .state('app.sdcTrnUatDeploymentSummary',
        {
            url: '/sdcTrnUatDeploymentSummary',
            title: 'sdcTrnUatDeploymentSummary',
            templateUrl: helper.basepath('ems.sdc/sdcTrnUatDeploymentSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
       .state('app.sdcTrnTestDeploymentView',
        {
            url: '/sdcTrnTestDeploymentView',
            title: 'sdcTrnTestDeploymentView',
            templateUrl: helper.basepath('ems.sdc/sdcTrnTestDeploymentView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.sdcTrnUatDeploymentView',
        {
            url: '/sdcTrnUatDeploymentView',
            title: 'sdcTrnUatDeploymentView',
            templateUrl: helper.basepath('ems.sdc/sdcTrnUatDeploymentView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
         .state('app.sdcTrnLiveSummary',
        {
            url: '/sdcTrnLiveSummary',
            title: 'sdcTrnLiveSummary',
            templateUrl: helper.basepath('ems.sdc/sdcTrnLiveSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
         .state('app.sdcTrnLiveDeploymentSummary',
        {
            url: '/sdcTrnLiveDeploymentSummary',
            title: 'sdcTrnLiveDeploymentSummary',
            templateUrl: helper.basepath('ems.sdc/sdcTrnLiveDeploymentSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
            .state('app.sdcTrnLiveDeploymentView',
        {
            url: '/sdcTrnLiveDeploymentView',
            title: 'sdcTrnLiveDeploymentView',
            templateUrl: helper.basepath('ems.sdc/sdcTrnLiveDeploymentView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
          .state('app.sdcTrnLiveView',
        {
            url: '/sdcTrnLiveView',
            title: 'sdcTrnLiveView',
            templateUrl: helper.basepath('ems.sdc/sdcTrnLiveView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
         .state('app.sdcTrnUatView',
        {
            url: '/sdcTrnUatView',
            title: 'sdcTrnUatView',
            templateUrl: helper.basepath('ems.sdc/sdcTrnUatView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
            .state('app.sdcTrnTestView',
        {
            url: '/sdcTrnTestView',
            title: 'sdcTrnTestView',
            templateUrl: helper.basepath('ems.sdc/sdcTrnTestView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
             .state('app.idasTrnLsaReport',
        {
            url: '/idasTrnLsaReport',
            title: 'idasTrnLsaReport',
            templateUrl: helper.basepath('ems.idas/idasTrnLsaReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
         .state('app.osdTrnTicketRejectCancelSummary',
        {
            url: '/osdTrnTicketRejectCancelSummary',
            title: 'osdTrnTicketRejectCancelSummary',
            templateUrl: helper.basepath('ems.osd/osdTrnTicketRejectCancelSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.MstBankName',
        {
            url: '/MstBankName',
            title: 'MstBankName',
            templateUrl: helper.basepath('ems.master/MstBankName.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstProperty',
        {
            url: '/MstProperty',
            title: 'MstProperty',
            templateUrl: helper.basepath('ems.master/MstProperty.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstClientDetails',
        {
            url: '/MstClientDetails',
            title: 'MstClientDetails',
            templateUrl: helper.basepath('ems.master/MstClientDetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstStartCreditUnderwriting', {
            url: '/MstStartCreditUnderwriting',
            title: 'MstStartCreditUnderwriting',
            templateUrl: helper.basepath('ems.master/MstStartCreditUnderwriting.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditCompanyDtlAdd', {
            url: '/MstCreditCompanyDtlAdd',
            title: 'MstCreditCompanyDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCreditCompanyDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditEconomicCapitalAdd', {
            url: '/MstCreditEconomicCapitalAdd',
            title: 'MstCreditEconomicCapitalAdd',
            templateUrl: helper.basepath('ems.master/MstCreditEconomicCapitalAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditPSLDataFlaggingAdd', {
            url: '/MstCreditPSLDataFlaggingAdd',
            title: 'MstCreditPSLDataFlaggingAdd',
            templateUrl: helper.basepath('ems.master/MstCreditPSLDataFlaggingAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstRMInstitutionView', {
            url: '/MstRMInstitutionView',
            title: 'MstRMInstitutionView',
            templateUrl: helper.basepath('ems.master/MstRMInstitutionView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstRMIndividualView', {
            url: '/MstRMIndividualView',
            title: 'MstRMIndividualView',
            templateUrl: helper.basepath('ems.master/MstRMIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstRMMemberView', {
            url: '/MstRMMemberView',
            title: 'MstRMMemberView',
            templateUrl: helper.basepath('ems.master/MstRMMemberView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditSuppliersDtlAdd', {
            url: '/MstCreditSuppliersDtlAdd',
            title: 'MstCreditSuppliersDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCreditSuppliersDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditBuyerDtlAdd', {
            url: '/MstCreditBuyerDtlAdd',
            title: 'MstCreditBuyerDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCreditBuyerDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditBankAccountDtlAdd', {
            url: '/MstCreditBankAccountDtlAdd',
            title: 'MstCreditBankAccountDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCreditBankAccountDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditExistingBankDtlAdd', {
            url: '/MstCreditExistingBankDtlAdd',
            title: 'MstCreditExistingBankDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCreditExistingBankDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditRepaymentDtlAdd', {
            url: '/MstCreditRepaymentDtlAdd',
            title: 'MstCreditRepaymentDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCreditRepaymentDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditObservationAdd', {
            url: '/MstCreditObservationAdd',
            title: 'MstCreditObservationAdd',
            templateUrl: helper.basepath('ems.master/MstCreditObservationAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualDtlAdd', {
            url: '/MstCreditIndividualDtlAdd',
            title: 'MstCreditIndividualDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualBankAcctAdd', {
            url: '/MstCreditIndividualBankAcctAdd',
            title: 'MstCreditIndividualBankAcctAdd',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualBankAcctAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualExistingBankAdd', {
            url: '/MstCreditIndividualExistingBankAdd',
            title: 'MstCreditIndividualExistingBankAdd',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualExistingBankAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualRepaymentAdd', {
            url: '/MstCreditIndividualRepaymentAdd',
            title: 'MstCreditIndividualRepaymentAdd',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualRepaymentAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualPSLDataFlagAdd', {
            url: '/MstCreditIndividualPSLDataFlagAdd',
            title: 'MstCreditIndividualPSLDataFlagAdd',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualPSLDataFlagAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualObservationAdd', {
            url: '/MstCreditIndividualObservationAdd',
            title: 'MstCreditIndividualObservationAdd',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualObservationAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupDtlAdd', {
            url: '/MstCreditGroupDtlAdd',
            title: 'MstCreditGroupDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCreditGroupDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupBankAcctAdd', {
            url: '/MstCreditGroupBankAcctAdd',
            title: 'MstCreditGroupBankAcctAdd',
            templateUrl: helper.basepath('ems.master/MstCreditGroupBankAcctAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupExistingBankAdd', {
            url: '/MstCreditGroupExistingBankAdd',
            title: 'MstCreditGroupExistingBankAdd',
            templateUrl: helper.basepath('ems.master/MstCreditGroupExistingBankAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupRepaymentAdd', {
            url: '/MstCreditGroupRepaymentAdd',
            title: 'MstCreditGroupRepaymentAdd',
            templateUrl: helper.basepath('ems.master/MstCreditGroupRepaymentAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupPSLDataFlagAdd', {
            url: '/MstCreditGroupPSLDataFlagAdd',
            title: 'MstCreditGroupPSLDataFlagAdd',
            templateUrl: helper.basepath('ems.master/MstCreditGroupPSLDataFlagAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupObservationAdd', {
            url: '/MstCreditGroupObservationAdd',
            title: 'MstCreditGroupObservationAdd',
            templateUrl: helper.basepath('ems.master/MstCreditGroupObservationAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditEconomicCapitalEdit', {
            url: '/MstCreditEconomicCapitalEdit',
            title: 'MstCreditEconomicCapitalEdit',
            templateUrl: helper.basepath('ems.master/MstCreditEconomicCapitalEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditPSLDataFlaggingEdit', {
            url: '/MstCreditPSLDataFlaggingEdit',
            title: 'MstCreditPSLDataFlaggingEdit',
            templateUrl: helper.basepath('ems.master/MstCreditPSLDataFlaggingEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditBuyerDtlEdit', {
            url: '/MstCreditBuyerDtlEdit',
            title: 'MstCreditBuyerDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCreditBuyerDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditSuppliersDtlEdit', {
            url: '/MstCreditSuppliersDtlEdit',
            title: 'MstCreditSuppliersDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCreditSuppliersDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditBankAccountDtlEdit', {
            url: '/MstCreditBankAccountDtlEdit',
            title: 'MstCreditBankAccountDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCreditBankAccountDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditExistingBankDtlEdit', {
            url: '/MstCreditExistingBankDtlEdit',
            title: 'MstCreditExistingBankDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCreditExistingBankDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditRepaymentDtlEdit', {
            url: '/MstCreditRepaymentDtlEdit',
            title: 'MstCreditRepaymentDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCreditRepaymentDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualBureauEdit', {
            url: '/MstCreditIndividualBureauEdit',
            title: 'MstCreditIndividualBureauEdit',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualBureauEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualBankAcctEdit', {
            url: '/MstCreditIndividualBankAcctEdit',
            title: 'MstCreditIndividualBankAcctEdit',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualBankAcctEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualExistingBankEdit', {
            url: '/MstCreditIndividualExistingBankEdit',
            title: 'MstCreditIndividualExistingBankEdit',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualExistingBankEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualRepaymentEdit', {
            url: '/MstCreditIndividualRepaymentEdit',
            title: 'MstCreditIndividualRepaymentEdit',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualRepaymentEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualPSLDataFlagEdit', {
            url: '/MstCreditIndividualPSLDataFlagEdit',
            title: 'MstCreditIndividualPSLDataFlagEdit',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualPSLDataFlagEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupBureauEdit', {
            url: '/MstCreditGroupBureauEdit',
            title: 'MstCreditGroupBureauEdit',
            templateUrl: helper.basepath('ems.master/MstCreditGroupBureauEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupBankAcctEdit', {
            url: '/MstCreditGroupBankAcctEdit',
            title: 'MstCreditGroupBankAcctEdit',
            templateUrl: helper.basepath('ems.master/MstCreditGroupBankAcctEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupExistingBankEdit', {
            url: '/MstCreditGroupExistingBankEdit',
            title: 'MstCreditGroupExistingBankEdit',
            templateUrl: helper.basepath('ems.master/MstCreditGroupExistingBankEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupRepaymentEdit', {
            url: '/MstCreditGroupRepaymentEdit',
            title: 'MstCreditGroupRepaymentEdit',
            templateUrl: helper.basepath('ems.master/MstCreditGroupRepaymentEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupPSLDataFlagEdit', {
            url: '/MstCreditGroupPSLDataFlagEdit',
            title: 'MstCreditGroupPSLDataFlagEdit',
            templateUrl: helper.basepath('ems.master/MstCreditGroupPSLDataFlagEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditCompanyDtlView', {
            url: '/MstCreditCompanyDtlView',
            title: 'MstCreditCompanyDtlView',
            templateUrl: helper.basepath('ems.master/MstCreditCompanyDtlView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
      
        .state('app.legalsrreport',
        {
            url: '/legalsrreport',
            title: 'legalsrreport',
            templateUrl: helper.basepath('ems.lgl/legalsrreport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstCreditIndividualDtlView', {
            url: '/MstCreditIndividualDtlView',
            title: 'MstCreditIndividualDtlView',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualDtlView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupDtlView', {
            url: '/MstCreditGroupDtlView',
            title: 'MstCreditGroupDtlView',
            templateUrl: helper.basepath('ems.master/MstCreditGroupDtlView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditAssessedScoreAdd', {
            url: '/MstCreditAssessedScoreAdd',
            title: 'MstCreditAssessedScoreAdd',
            templateUrl: helper.basepath('ems.master/MstCreditAssessedScoreAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstRMAssessedScoreView', {
            url: '/MstRMAssessedScoreView',
            title: 'MstRMAssessedScoreView',
            templateUrl: helper.basepath('ems.master/MstRMAssessedScoreView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditAssessedScoreEdit', {
            url: '/MstCreditAssessedScoreEdit',
            title: 'MstCreditAssessedScoreEdit',
            templateUrl: helper.basepath('ems.master/MstCreditAssessedScoreEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditAssessedScoreView', {
            url: '/MstCreditAssessedScoreView',
            title: 'MstCreditAssessedScoreView',
            templateUrl: helper.basepath('ems.master/MstCreditAssessedScoreView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditVisitReportAdd', {
            url: '/MstCreditVisitReportAdd',
            title: 'MstCreditVisitReportAdd',
            templateUrl: helper.basepath('ems.master/MstCreditVisitReportAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstRMVisitReportView', {
            url: '/MstRMVisitReportView',
            title: 'MstRMVisitReportView',
            templateUrl: helper.basepath('ems.master/MstRMVisitReportView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditVisitReportEdit', {
            url: '/MstCreditVisitReportEdit',
            title: 'MstCreditVisitReportEdit',
            templateUrl: helper.basepath('ems.master/MstCreditVisitReportEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditVisitReportView', {
            url: '/MstCreditVisitReportView',
            title: 'MstCreditVisitReportView',
            templateUrl: helper.basepath('ems.master/MstCreditVisitReportView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGeneralDtlEdit', {
            url: '/MstCreditGeneralDtlEdit',
            title: 'MstCreditGeneralDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCreditGeneralDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditInstitutionDtlEdit', {
            url: '/MstCreditInstitutionDtlEdit',
            title: 'MstCreditInstitutionDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCreditInstitutionDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualDtlEdit', {
            url: '/MstCreditIndividualDtlEdit',
            title: 'MstCreditIndividualDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupDtlEdit', {
            url: '/MstCreditGroupDtlEdit',
            title: 'MstCreditGroupDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCreditGroupDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstApplicationReport', {
            url: '/MstApplicationReport',
            title: 'MstApplicationReport',
            templateUrl: helper.basepath('ems.master/MstApplicationReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditProductChargesDtlEdit', {
            url: '/MstCreditProductChargesDtlEdit',
            title: 'MstCreditProductChargesDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCreditProductChargesDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditLoanDtlEdit', {
            url: '/MstCreditLoanDtlEdit',
            title: 'MstCreditLoanDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCreditLoanDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditServicesDtlEdit', {
            url: '/MstCreditServicesDtlEdit',
            title: 'MstCreditServicesDtlEdit',
            templateUrl: helper.basepath('ems.master/MstCreditServicesDtlEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
	    .state('app.idasTrnCourierReport',
        {
            url: '/idasTrnCourierReport',
            title: 'idasTrnCourierReport',
            templateUrl: helper.basepath('ems.idas/idasTrnCourierReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstCreditHypothecationEdit', {
            url: '/MstCreditHypothecationEdit',
            title: 'MstCreditHypothecationEdit',
            templateUrl: helper.basepath('ems.master/MstCreditHypothecationEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

         .state('app.idasCourierMgmtAckList',
        {
            url: '/idasCourierMgmtAckList',
            title: 'idasCourierMgmtAckList',
            templateUrl: helper.basepath('ems.idas/idasCourierMgmtAckList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
         .state('app.MstUDCMakerSummary', {
             url: '/MstUDCMakerSummary',
             title: 'MstUDCMakerSummary',
             templateUrl: helper.basepath('ems.master/MstUDCMakerSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
        .state('app.MstUDCMakerAdd', {
            url: '/MstUDCMakerAdd',
            title: 'MstUDCMakerAdd',
            templateUrl: helper.basepath('ems.master/MstUDCMakerAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstUDCMakerEdit', {
            url: '/MstUDCMakerEdit',
            title: 'MstUDCMakerEdit',
            templateUrl: helper.basepath('ems.master/MstUDCMakerEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstUDCMakerEditCheque', {
            url: '/MstUDCMakerEditCheque',
            title: 'MstUDCMakerEditCheque',
            templateUrl: helper.basepath('ems.master/MstUDCMakerEditCheque.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstUDCMakerAddCheque', {
            url: '/MstUDCMakerAddCheque',
            title: 'MstUDCMakerAddCheque',
            templateUrl: helper.basepath('ems.master/MstUDCMakerAddCheque.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstUDCMakerView', {
            url: '/MstUDCMakerView',
            title: 'MstUDCMakerView',
            templateUrl: helper.basepath('ems.master/MstUDCMakerView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
         .state('app.MstCreditCompanyAPIAdd', {
             url: '/MstCreditCompanyAPIAdd',
             title: 'MstCreditCompanyAPIAdd',
             templateUrl: helper.basepath('ems.master/MstCreditCompanyAPIAdd.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
         .state('app.MstCreditIndividualAPI', {
             url: '/MstCreditIndividualAPI',
             title: 'MstCreditIndividualAPI',
             templateUrl: helper.basepath('ems.master/MstCreditIndividualAPI.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
         .state('app.idasTrnCollateralReport', {
             url: '/idasTrnCollateralReport',
             title: 'idasTrnCollateralReport',
             templateUrl: helper.basepath('ems.idas/idasTrnCollateralReport.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
         .state('app.IECDetailedProfileView', {
             url: '/IECDetailedProfileView',
             title: 'IECDetailedProfileView',
             templateUrl: helper.basepath('ems.master/IECDetailedProfileView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
        .state('app.osdTrnTaggedRequestSummary', {
            url: '/osdTrnTaggedRequestSummary',
            title: 'osdTrnTaggedRequestSummary',
            templateUrl: helper.basepath('ems.osd/osdTrnTaggedRequestSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.osdTrnForwardTransferSummary', {
            url: '/osdTrnForwardTransferSummary',
            title: 'osdTrnForwardTransferSummary',
            templateUrl: helper.basepath('ems.osd/osdTrnForwardTransferSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.osdTrnReopenRequestSummary', {
            url: '/osdTrnReopenRequestSummary',
            title: 'osdTrnReopenRequestSummary',
            templateUrl: helper.basepath('ems.osd/osdTrnReopenRequestSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.OsdTrnAllotedMyTicket',
        {
            url: '/OsdTrnAllotedMyTicket',
            title: 'OsdTrnAllotedMyTicket',
            templateUrl: helper.basepath('ems.osd/OsdTrnAllotedMyTicket.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.OsdTrnWorkMyTicket',
        {
            url: '/OsdTrnWorkMyTicket',
            title: 'OsdTrnWorkMyTicket',
            templateUrl: helper.basepath('ems.osd/OsdTrnWorkMyTicket.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.OsdTrnExternalMyTicket',
        {
            url: '/OsdTrnExternalMyTicket',
            title: 'OsdTrnExternalMyTicket',
            templateUrl: helper.basepath('ems.osd/OsdTrnExternalMyTicket.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.OsdTrnInternalMyTicket',
        {
            url: '/OsdTrnInternalMyTicket',
            title: 'OsdTrnInternalMyTicket',
            templateUrl: helper.basepath('ems.osd/OsdTrnInternalMyTicket.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.OsdTrnCompletedMyTicket',
        {
            url: '/OsdTrnCompletedMyTicket',
            title: 'OsdTrnCompletedMyTicket',
            templateUrl: helper.basepath('ems.osd/OsdTrnCompletedMyTicket.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.OsdTrnClosedMyTicket',
        {
            url: '/OsdTrnClosedMyTicket',
            title: 'OsdTrnClosedMyTicket',
            templateUrl: helper.basepath('ems.osd/OsdTrnClosedMyTicket.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.GSPGSTReturnFilingView', {
            url: '/GSPGSTReturnFilingView',
            title: 'GSPGSTReturnFilingView',
            templateUrl: helper.basepath('ems.master/GSPGSTReturnFilingView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.GSPGSTINAuthenticationView', {
            url: '/GSPGSTINAuthenticationView',
            title: 'GSPGSTINAuthenticationView',
            templateUrl: helper.basepath('ems.master/GSPGSTINAuthenticationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.GSTAuthenticationView', {
            url: '/GSTAuthenticationView',
            title: 'GSTAuthenticationView',
            templateUrl: helper.basepath('ems.master/GSTAuthenticationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditCommitteeSummary', {
            url: '/MstCreditCommitteeSummary',
            title: 'MstCreditCommitteeSummary',
            templateUrl: helper.basepath('ems.master/MstCreditCommitteeSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCcCommitteeApplicationView', {
            url: '/MstCcCommitteeApplicationView',
            title: 'MstCcCommitteeApplicationView',
            templateUrl: helper.basepath('ems.master/MstCcCommitteeApplicationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCcCommitteeKycView', {
            url: '/MstCcCommitteeKycView',
            title: 'MstCcCommitteeKycView',
            templateUrl: helper.basepath('ems.master/MstCcCommitteeKycView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCcCommitteeInstitutionView', {
            url: '/MstCcCommitteeInstitutionView',
            title: 'MstCcCommitteeInstitutionView',
            templateUrl: helper.basepath('ems.master/MstCcCommitteeInstitutionView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCcCommitteeIndividualView', {
            url: '/MstCcCommitteeIndividualView',
            title: 'MstCcCommitteeIndividualView',
            templateUrl: helper.basepath('ems.master/MstCcCommitteeIndividualView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCcCommitteeGroupView', {
            url: '/MstCcCommitteeGroupView',
            title: 'MstCcCommitteeGroupView',
            templateUrl: helper.basepath('ems.master/MstCcCommitteeGroupView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCCMeetingSchedule', {
            url: '/MstCCMeetingSchedule',
            title: 'MstCCMeetingSchedule',
            templateUrl: helper.basepath('ems.master/MstCCMeetingSchedule.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCCscheduledSummary', {
            url: '/MstCCscheduledSummary',
            title: 'MstCCscheduledSummary',
            templateUrl: helper.basepath('ems.master/MstCCscheduledSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.RCAuthAdvancedView', {
            url: '/RCAuthAdvancedView',
            title: 'RCAuthAdvancedView',
            templateUrl: helper.basepath('ems.master/RCAuthAdvancedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.PropertyTaxView', {
            url: '/PropertyTaxView',
            title: 'PropertyTaxView',
            templateUrl: helper.basepath('ems.master/PropertyTaxView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCcScheduledMeetingSummary', {
            url: '/MstCcScheduledMeetingSummary',
            title: 'MstCcScheduledMeetingSummary',
            templateUrl: helper.basepath('ems.master/MstCcScheduledMeetingSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'jquery-ui', 'jquery-ui-widgets', 'moment', 'fullcalendar')
        })
        .state('app.MstStartScheduledMeeting', {
            url: '/MstStartScheduledMeeting',
            title: 'MstStartScheduledMeeting',
            templateUrl: helper.basepath('ems.master/MstStartScheduledMeeting.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCCMeetingReschedule', {
            url: '/MstCCMeetingReschedule',
            title: 'MstCCMeetingReschedule',
            templateUrl: helper.basepath('ems.master/MstCCMeetingReschedule.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.CreditUnderwritingKycLogView', {
            url: '/CreditUnderwritingKycLogView',
            title: 'CreditUnderwritingKycLogView',
            templateUrl: helper.basepath('ems.master/CreditUnderwritingKycLogView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstProgram', {
            url: '/MstProgram',
            title: 'MstProgram',
            templateUrl: helper.basepath('ems.master/MstProgram.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.SysMstTriggerUser', {
            url: '/SysMstTriggerUser',
            title: 'SysMstTriggerUser',
            templateUrl: helper.basepath('ems.system/SysMstTriggerUser.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
         .state('app.SysMstBloodGroup', {
             url: '/SysMstBloodGroup',
             title: 'SysMstBloodGroup',
             templateUrl: helper.basepath('ems.system/SysMstBloodGroup.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
            .state('app.SysMstPhysicalStatus', {
                url: '/SysMstPhysicalStatus',
                title: 'SysMstPhysicalStatus',
                templateUrl: helper.basepath('ems.system/SysMstPhysicalStatus.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
            })
        .state('app.SysMstBaseLocation', {
            url: '/SysMstBaseLocation',
            title: 'SysMstBaseLocation',
            templateUrl: helper.basepath('ems.system/SysMstBaseLocation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.SysMstSalutation', {
            url: '/SysMstSalutation',
            title: 'SysMstSalutation',
            templateUrl: helper.basepath('ems.system/SysMstSalutation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.SysMstClientRole', {
            url: '/SysMstClientRole',
            title: 'SysMstClientRole',
            templateUrl: helper.basepath('ems.system/SysMstClientRole.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.SysMstCalendarGroup', {
            url: '/SysMstCalendarGroup',
            title: 'SysMstCalendarGroup',
            templateUrl: helper.basepath('ems.system/SysMstCalendarGroup.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.SysMstProjects', {
            url: '/SysMstProjects',
            title: 'SysMstProjects',
            templateUrl: helper.basepath('ems.system/SysMstProjects.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCCCompletedSummary', {
            url: '/MstCCCompletedSummary',
            title: 'MstCCCompletedSummary',
            templateUrl: helper.basepath('ems.master/MstCCCompletedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCcCompletedScheduledMeeting', {
            url: '/MstCcCompletedScheduledMeeting',
            title: 'MstCcCompletedScheduledMeeting',
            templateUrl: helper.basepath('ems.master/MstCcCompletedScheduledMeeting.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCcScheduledMeetingDtlView', {
            url: '/MstCcScheduledMeetingDtlView',
            title: 'MstCcScheduledMeetingDtlView',
            templateUrl: helper.basepath('ems.master/MstCcScheduledMeetingDtlView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCAMGenerate', {
            url: '/MstCAMGenerate',
            title: 'MstCAMGenerate',
            templateUrl: helper.basepath('ems.master/MstCAMGenerate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditBankStatementAnalysisAdd', {
            url: '/MstCreditBankStatementAnalysisAdd',
            title: 'MstCreditBankStatementAnalysisAdd',
            templateUrl: helper.basepath('ems.master/MstCreditBankStatementAnalysisAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditIndividualBankStatementAnalysisAdd', {
            url: '/MstCreditIndividualBankStatementAnalysisAdd',
            title: 'MstCreditIndividualBankStatementAnalysisAdd',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualBankStatementAnalysisAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditGroupBankStatementAnalysisAdd', {
            url: '/MstCreditGroupBankStatementAnalysisAdd',
            title: 'MstCreditGroupBankStatementAnalysisAdd',
            templateUrl: helper.basepath('ems.master/MstCreditGroupBankStatementAnalysisAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.SysMstClusterMapping', {
            url: '/SysMstClusterMapping',
            title: 'SysMstClusterMapping',
            templateUrl: helper.basepath('ems.system/SysMstClusterMapping.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.SysMstRegionMapping', {
            url: '/SysMstRegionMapping',
            title: 'SysMstRegionMapping',
            templateUrl: helper.basepath('ems.system/SysMstRegionMapping.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.SysMstZoneMapping', {
            url: '/SysMstZoneMapping',
            title: 'SysMstZoneMapping',
            templateUrl: helper.basepath('ems.system/SysMstZoneMapping.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.SysMstRegionHead', {
            url: '/SysMstRegionHead',
            title: 'SysMstRegionHead',
            templateUrl: helper.basepath('ems.system/SysMstRegionHead.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.SysMstBusinessHead', {
            url: '/SysMstBusinessHead',
            title: 'SysMstBusinessHead',
            templateUrl: helper.basepath('ems.system/SysMstBusinessHead.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.SysMstGroupBusinessHead', {
            url: '/SysMstGroupBusinessHead',
            title: 'SysMstGroupBusinessHead',
            templateUrl: helper.basepath('ems.system/SysMstGroupBusinessHead.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.SysMstClusterHead', {
            url: '/SysMstClusterHead',
            title: 'SysMstClusterHead',
            templateUrl: helper.basepath('ems.system/SysMstClusterHead.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
         .state('app.SysMstZonalHead', {
             url: '/SysMstZonalHead',
             title: 'SysMstZonalHead',
             templateUrl: helper.basepath('ems.system/SysMstZonalHead.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })

         .state('app.SysMstProductHead', {
             url: '/SysMstProductHead',
             title: 'SysMstProductHead',
             templateUrl: helper.basepath('ems.system/SysMstProductHead.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
         .state('app.SysMstOtherApplication', {
             url: '/SysMstOtherApplication',
             title: 'SysMstOtherApplication',
             templateUrl: helper.basepath('ems.system/SysMstOtherApplication.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
        .state('app.OtherApplicationDashboard', {
            url: '/OtherApplicationDashboard',
            title: 'OtherApplicationDashboard',
            templateUrl: helper.basepath('ems.system/OtherApplicationDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditFsaDetailAdd', {
            url: '/MstCreditFsaDetailAdd',
            title: 'MstCreditFsaDetailAdd',
            templateUrl: helper.basepath('ems.master/MstCreditFsaDetailAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.idasTrnCadDashboard', {
            url: '/idasTrnCadDashboard',
            title: 'idasTrnCadDashboard',
            templateUrl: helper.basepath('ems.idas/idasTrnCadDashboard.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditFSAView', {
            url: '/MstCreditFSAView',
            title: 'MstCreditFSAView',
            templateUrl: helper.basepath('ems.master/MstCreditFSAView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.iasnTrnConsolidatedReport', {
            url: '/iasnTrnConsolidatedReport',
            title: 'iasnTrnConsolidatedReport',
            templateUrl: helper.basepath('ems.iasn/iasnTrnConsolidatedReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
	    .state('app.SystemDashboard', {
	        url: '/SystemDashboard',
	        title: 'SystemDashboard',
	        templateUrl: helper.basepath('ems.system/SystemDashboard.html?ver=' + version + '"'),
	        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
	    })
        .state('app.idasMstDocTemplate', {
            url: '/idasMstDocTemplate',
            title: 'idasMstDocTemplate',
            templateUrl: helper.basepath('ems.idas/idasMstDocTemplate.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.idasTrnPreFilGeneration', {
            url: '/idasTrnPreFilGeneration',
            title: 'idasTrnPreFilGeneration',
            templateUrl: helper.basepath('ems.idas/idasTrnPreFilGeneration.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.idasTrnDocumentGeneration', {
            url: '/idasTrnDocumentGeneration',
            title: 'idasTrnDocumentGeneration',
            templateUrl: helper.basepath('ems.idas/idasTrnDocumentGeneration.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.idasTrnPreFilManagement', {
            url: '/idasTrnPreFilManagement',
            title: 'idasTrnPreFilManagement',
            templateUrl: helper.basepath('ems.idas/idasTrnPreFilManagement.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
       	.state('app.MstCallType', {
       	    url: '/MstCallType',
       	    title: 'MstCallType',
       	    templateUrl: helper.basepath('ems.master/MstCallType.html?ver=' + version + '"'),
       	    resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
       	})
	    .state('app.MstSourceofContact', {
	        url: '/MstSourceofContact',
	        title: 'MstSourceofContact',
	        templateUrl: helper.basepath('ems.master/MstSourceofContact.html?ver=' + version + '"'),
	        resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
	    })
        .state('app.MstApplicationCreationRMApproval', {
            url: '/MstApplicationCreationRMApproval',
            title: 'MstApplicationCreationRMApproval',
            templateUrl: helper.basepath('ems.master/MstApplicationCreationRMApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstRejectedApplicationSummary', {
            url: '/MstRejectedApplicationSummary',
            title: 'MstRejectedApplicationSummary',
            templateUrl: helper.basepath('ems.master/MstRejectedApplicationSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstHoldApplicationSummary', {
            url: '/MstHoldApplicationSummary',
            title: 'MstHoldApplicationSummary',
            templateUrl: helper.basepath('ems.master/MstHoldApplicationSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstApprovedApplicationSummary', {
            url: '/MstApprovedApplicationSummary',
            title: 'MstApprovedApplicationSummary',
            templateUrl: helper.basepath('ems.master/MstApprovedApplicationSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstBusinessApprovalSummary', {
            url: '/MstBusinessApprovalSummary',
            title: 'MstBusinessApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstBusinessApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstBusinessRejectedSummary', {
            url: '/MstBusinessRejectedSummary',
            title: 'MstBusinessRejectedSummary',
            templateUrl: helper.basepath('ems.master/MstBusinessRejectedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstBusinessHoldSummary', {
            url: '/MstBusinessHoldSummary',
            title: 'MstBusinessHoldSummary',
            templateUrl: helper.basepath('ems.master/MstBusinessHoldSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstBusinessApprovedSummary', {
            url: '/MstBusinessApprovedSummary',
            title: 'MstBusinessApprovedSummary',
            templateUrl: helper.basepath('ems.master/MstBusinessApprovedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstBusinessApproval', {
            url: '/MstBusinessApproval',
            title: 'MstBusinessApproval',
            templateUrl: helper.basepath('ems.master/MstBusinessApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstTelecallingFunction', {
            url: '/MstTelecallingFunction',
            title: 'MstTelecallingFunction',
            templateUrl: helper.basepath('ems.master/MstTelecallingFunction.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstCallReceivedNumber', {
            url: '/MstCallReceivedNumber',
            title: 'MstCallReceivedNumber',
            templateUrl: helper.basepath('ems.master/MstCallReceivedNumber.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
        })
        .state('app.MstCreditMappingSummary', {
            url: '/MstCreditMappingSummary',
            title: 'MstCreditMappingSummary',
            templateUrl: helper.basepath('ems.master/MstCreditMappingSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'textAngular', 'localytics.directives')
        })
         .state('app.osdTrnCloseRequestSummary', {
             url: '/osdTrnCloseRequestSummary',
             title: 'osdTrnCloseRequestSummary',
             templateUrl: helper.basepath('ems.osd/osdTrnCloseRequestSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
        .state('app.MstVisitorSummary', {
            url: '/MstVisitorSummary',
            title: 'MstVisitorSummary',
            templateUrl: helper.basepath('ems.master/MstVisitorSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'angularGrid')
        })
        .state('app.MstVisitorAdd', {
            url: '/MstVisitorAdd',
            title: 'MstVisitorAdd',
            templateUrl: helper.basepath('ems.master/MstVisitorAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.MstVisitorEdit', {
            url: '/MstVisitorEdit',
            title: 'MstVisitorEdit',
            templateUrl: helper.basepath('ems.master/MstVisitorEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })
        .state('app.MstMyApplicationsSummary', {
            url: '/MstMyApplicationsSummary',
            title: 'MstMyApplicationsSummary',
            templateUrl: helper.basepath('ems.master/MstMyApplicationsSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSubmittedtoApprovalSummary', {
            url: '/MstSubmittedtoApprovalSummary',
            title: 'MstSubmittedtoApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstSubmittedtoApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSubmittedtoCCSummary', {
            url: '/MstSubmittedtoCCSummary',
            title: 'MstSubmittedtoCCSummary',
            templateUrl: helper.basepath('ems.master/MstSubmittedtoCCSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCCSkippedApplicationSummary', {
            url: '/MstCCSkippedApplicationSummary',
            title: 'MstCCSkippedApplicationSummary',
            templateUrl: helper.basepath('ems.master/MstCCSkippedApplicationSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstRejectandHoldSummary', {
            url: '/MstRejectandHoldSummary',
            title: 'MstRejectandHoldSummary',
            templateUrl: helper.basepath('ems.master/MstRejectandHoldSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditApprovalSummary', {
            url: '/MstCreditApprovalSummary',
            title: 'MstCreditApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstCreditApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditApprovedSummary', {
            url: '/MstCreditApprovedSummary',
            title: 'MstCreditApprovedSummary',
            templateUrl: helper.basepath('ems.master/MstCreditApprovedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditSubmittedtoCCSummary', {
            url: '/MstCreditSubmittedtoCCSummary',
            title: 'MstCreditSubmittedtoCCSummary',
            templateUrl: helper.basepath('ems.master/MstCreditSubmittedtoCCSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditCCSkippedSummary', {
            url: '/MstCreditCCSkippedSummary',
            title: 'MstCreditCCSkippedSummary',
            templateUrl: helper.basepath('ems.master/MstCreditCCSkippedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditRejectandHoldSummary', {
            url: '/MstCreditRejectandHoldSummary',
            title: 'MstCreditRejectandHoldSummary',
            templateUrl: helper.basepath('ems.master/MstCreditRejectandHoldSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditApproval', {
            url: '/MstCreditApproval',
            title: 'MstCreditApproval',
            templateUrl: helper.basepath('ems.master/MstCreditApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditQueryStatus', {
            url: '/MstCreditQueryStatus',
            title: 'MstCreditQueryStatus',
            templateUrl: helper.basepath('ems.master/MstCreditQueryStatus.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.osdBamTicketCompletedSummary', {
            url: '/osdBamTicketCompletedSummary',
            title: 'osdBamTicketCompletedSummary',
            templateUrl: helper.basepath('ems.osd/osdBamTicketCompletedSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.HighmarkReport', {
            url: '/HighmarkReport',
            title: 'HighmarkReport',
            templateUrl: helper.basepath('ems.master/HighmarkReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
 	    .state('app.idasTrnNocAndNdc', {
 	       url: '/idasTrnNocAndNdc',
 	       title: 'idasTrnNocAndNdc',
 	       templateUrl: helper.basepath('ems.idas/idasTrnNocAndNdc.html?ver=' + version + '"'),
 	       resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables')
 	    })
        .state('app.MstApplSubmittedtoCCSummary', {
            url: '/MstApplSubmittedtoCCSummary',
            title: 'MstApplSubmittedtoCCSummary',
            templateUrl: helper.basepath('ems.master/MstApplSubmittedtoCCSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditAllocationReport', {
            url: '/MstCreditAllocationReport',
            title: 'MstCreditAllocationReport',
            templateUrl: helper.basepath('ems.master/MstCreditAllocationReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
         .state('app.MstTaggedVisitorSummary', {
             url: '/MstTaggedVisitorSummary',
             title: 'MstTaggedVisitorSummary',
             templateUrl: helper.basepath('ems.master/MstTaggedVisitorSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
        .state('app.MstHistoryVisitorSummary', {
            url: '/MstHistoryVisitorSummary',
            title: 'MstHistoryVisitorSummary',
            templateUrl: helper.basepath('ems.master/MstHistoryVisitorSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
         .state('app.MstVisitorView', {
             url: '/MstVisitorView',
             title: 'MstVisitorView',
             templateUrl: helper.basepath('ems.master/MstVisitorView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
         })
         .state('app.idasTrnNocAndNdcView', {
             url: '/idasTrnNocAndNdcView',
             title: 'idasTrnNocAndNdcView',
             templateUrl: helper.basepath('ems.idas/idasTrnNocAndNdcView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
         })
       .state('app.idasTrnNocAndNdcAdd', {
            url: '/idasTrnNocAndNdcAdd',
            title: 'idasTrnNocAndNdcAdd',
            templateUrl: helper.basepath('ems.idas/idasTrnNocAndNdcAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.idasTrnNocAndNdcEdit', {
            url: '/idasTrnNocAndNdcEdit',
            title: 'idasTrnNocAndNdcEdit',
            templateUrl: helper.basepath('ems.idas/idasTrnNocAndNdcEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstTaggedMemberSummary', {
            url: '/MstTaggedMemberSummary',
            title: 'MstTaggedMemberSummary',
            templateUrl: helper.basepath('ems.master/MstTaggedMemberSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCompletedCallSummary', {
            url: '/MstCompletedCallSummary',
            title: 'MstCompletedCallSummary',
            templateUrl: helper.basepath('ems.master/MstCompletedCallSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstTransferCallSummary', {
            url: '/MstTransferCallSummary',
            title: 'MstTransferCallSummary',
            templateUrl: helper.basepath('ems.master/MstTransferCallSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstInboundAdd', {
            url: '/MstInboundAdd',
            title: 'MstInboundAdd',
            templateUrl: helper.basepath('ems.master/MstInboundAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstTelecallingSummary', {
            url: '/MstTelecallingSummary',
            title: 'MstTelecallingSummary',
            templateUrl: helper.basepath('ems.master/MstTelecallingSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstInboundEdit', {
            url: '/MstInboundEdit',
            title: 'MstInboundEdit',
            templateUrl: helper.basepath('ems.master/MstInboundEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstTeleCallingCompletedCall', {
            url: '/MstTeleCallingCompletedCall',
            title: 'MstTeleCallingCompletedCall',
            templateUrl: helper.basepath('ems.master/MstTeleCallingCompletedCall.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstTeleCallingFollowupCall', {
            url: '/MstTeleCallingFollowupCall',
            title: 'MstTeleCallingFollowupCall',
            templateUrl: helper.basepath('ems.master/MstTeleCallingFollowupCall.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstTeleCallingClosedCall', {
            url: '/MstTeleCallingClosedCall',
            title: 'MstTeleCallingClosedCall',
            templateUrl: helper.basepath('ems.master/MstTeleCallingClosedCall.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstTeleCallingAssignView', {
            url: '/MstTeleCallingAssignView',
            title: 'MstTeleCallingAssignView',
            templateUrl: helper.basepath('ems.master/MstTeleCallingAssignView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstTeleCallingClosedView', {
            url: '/MstTeleCallingClosedView',
            title: 'MstTeleCallingClosedView',
            templateUrl: helper.basepath('ems.master/MstTeleCallingClosedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstTeleCallingCompletedView', {
            url: '/MstTeleCallingCompletedView',
            title: 'MstTeleCallingCompletedView',
            templateUrl: helper.basepath('ems.master/MstTeleCallingCompletedView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstMyAssignedCallSummary', {
            url: '/MstMyAssignedCallSummary',
            title: 'MstMyAssignedCallSummary',
            templateUrl: helper.basepath('ems.master/MstMyAssignedCallSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstTeleCallingClose', {
            url: '/MstTeleCallingClose',
            title: 'MstTeleCallingClose',
            templateUrl: helper.basepath('ems.master/MstTeleCallingClose.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCallResponse', {
            url: '/MstCallResponse',
            title: 'MstCallResponse',
            templateUrl: helper.basepath('ems.master/MstCallResponse.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstAssignedInboundCallSummary', {
            url: '/MstAssignedInboundCallSummary',
            title: 'MstAssignedInboundCallSummary',
            templateUrl: helper.basepath('ems.master/MstAssignedInboundCallSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCompletedInboundCallSummary', {
            url: '/MstCompletedInboundCallSummary',
            title: 'MstCompletedInboundCallSummary',
            templateUrl: helper.basepath('ems.master/MstCompletedInboundCallSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstFollowUpInboundCallSummary', {
            url: '/MstFollowUpInboundCallSummary',
            title: 'MstFollowUpInboundCallSummary',
            templateUrl: helper.basepath('ems.master/MstFollowUpInboundCallSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstClosedInboundCallSummary', {
            url: '/MstClosedInboundCallSummary',
            title: 'MstClosedInboundCallSummary',
            templateUrl: helper.basepath('ems.master/MstClosedInboundCallSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstWorkInprogressCallSummary', {
            url: '/MstWorkInprogressCallSummary',
            title: 'MstWorkInprogressCallSummary',
            templateUrl: helper.basepath('ems.master/MstWorkInprogressCallSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstAssignedCalls', {
            url: '/MstAssignedCalls',
            title: 'MstAssignedCalls',
            templateUrl: helper.basepath('ems.master/MstAssignedCalls.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstFollowUpCallSummary', {
            url: '/MstFollowUpCallSummary',
            title: 'MstFollowUpCallSummary',
            templateUrl: helper.basepath('ems.master/MstFollowUpCallSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstTaggedMemberView', {
            url: '/MstTaggedMemberView',
            title: 'MstTaggedMemberView',
            templateUrl: helper.basepath('ems.master/MstTaggedMemberView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstAssignedCallView', {
            url: '/MstAssignedCallView',
            title: 'MstAssignedCallView',
            templateUrl: helper.basepath('ems.master/MstAssignedCallView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstAssignedInboundCallView', {
            url: '/MstAssignedInboundCallView',
            title: 'MstAssignedInboundCallView',
            templateUrl: helper.basepath('ems.master/MstAssignedInboundCallView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstRejectedCallSummary', {
            url: '/MstRejectedCallSummary',
            title: 'MstRejectedCallSummary',
            templateUrl: helper.basepath('ems.master/MstRejectedCallSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstRejectedCallView', {
            url: '/MstRejectedCallView',
            title: 'MstRejectedCallView',
            templateUrl: helper.basepath('ems.master/MstRejectedCallView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstRejectedInboundCallSummary', {
            url: '/MstRejectedInboundCallSummary',
            title: 'MstRejectedInboundCallSummary',
            templateUrl: helper.basepath('ems.master/MstRejectedInboundCallSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstTeleCallingFollowupCallView', {
            url: '/MstTeleCallingFollowupCallView',
            title: 'MstTeleCallingFollowupCallView',
            templateUrl: helper.basepath('ems.master/MstTeleCallingFollowupCallView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.idasTrnNocAndNdcReport', {
            url: '/idasTrnNocAndNdcReport',
            title: 'idasTrnNocAndNdcReport',
            templateUrl: helper.basepath('ems.idas/idasTrnNocAndNdcReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstProductSummary', {
             url: '/MstProductSummary',
             title: 'MstProductSummary',
             templateUrl: helper.basepath('ems.master/MstProductSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
       .state('app.MstProductView', {
            url: '/MstProductView',
            title: 'MstProductView',
            templateUrl: helper.basepath('ems.master/MstProductView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstProductAdd', {
              url: '/MstProductAdd',
              title: 'MstProductAdd',
              templateUrl: helper.basepath('ems.master/MstProductAdd.html?ver=' + version + '"'),
              resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstProductEdit', {
            url: '/MstProductEdit',
            title: 'MstProductEdit',
            templateUrl: helper.basepath('ems.master/MstProductEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCADGroupSummary', {
             url: '/MstCADGroupSummary',
             title: 'MstCADGroupSummary',
             templateUrl: helper.basepath('ems.master/MstCADGroupSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.sdcTrnTestReport', {
            url: '/sdcTrnTestReport',
            title: 'sdcTrnTestReport',
            templateUrl: helper.basepath('ems.sdc/sdcTrnTestReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.sdcTrnUatReport', {
            url: '/sdcTrnUatReport',
            title: 'sdcTrnUatReport',
            templateUrl: helper.basepath('ems.sdc/sdcTrnUatReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.sdcTrnLiveReport', {
            url: '/sdcTrnLiveReport',
            title: 'sdcTrnLiveReport',
            templateUrl: helper.basepath('ems.sdc/sdcTrnLiveReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCADGroupAssignmentSummary', {
            url: '/MstCADGroupAssignmentSummary',
            title: 'MstCADGroupAssignmentSummary',
            templateUrl: helper.basepath('ems.master/MstCADGroupAssignmentSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCADGroupAssignmentAdd', {
            url: '/MstCADGroupAssignmentAdd',
            title: 'MstCADGroupAssignmentAdd',
            templateUrl: helper.basepath('ems.master/MstCADGroupAssignmentAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCADGroupAssignmentEdit', {
            url: '/MstCADGroupAssignmentEdit',
            title: 'MstCADGroupAssignmentEdit',
            templateUrl: helper.basepath('ems.master/MstCADGroupAssignmentEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmMstCheckpointGroupAdd', {
            url: '/AtmMstCheckpointGroupAdd',
            title: 'AtmMstCheckpointGroupAdd',
            templateUrl: helper.basepath('ems.audit/AtmMstCheckpointGroupAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.AtmMstAuditMappingAdd', {
            url: '/AtmMstAuditMappingAdd',
            title: 'AtmMstAuditMappingAdd',
            templateUrl: helper.basepath('ems.audit/AtmMstAuditMappingAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.AtmMstRiskCategory', {
            url: '/AtmMstRiskCategory',
            title: 'AtmMstRiskCategory',
            templateUrl: helper.basepath('ems.audit/AtmMstRiskCategory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmMstAuditType', {
            url: '/AtmMstAuditType',
            title: 'AtmMstAuditType',
            templateUrl: helper.basepath('ems.audit/AtmMstAuditType.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
         .state('app.AtmMstAuditDepartment', {
            url: '/AtmMstAuditDepartment',
            title: 'AtmMstAuditDepartment',
            templateUrl: helper.basepath('ems.audit/AtmMstAuditDepartment.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })
        .state('app.MstPendingCADReview', {
            url: '/MstPendingCADReview',
            title: 'MstPendingCADReview',
            templateUrl: helper.basepath('ems.master/MstPendingCADReview.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadAcceptedCustomers', {
            url: '/MstCadAcceptedCustomers',
            title: 'MstCadAcceptedCustomers',
            templateUrl: helper.basepath('ems.master/MstCadAcceptedCustomers.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstSentBackToUnderwriting', {
            url: '/MstSentBackToUnderwriting',
            title: 'MstSentBackToUnderwriting',
            templateUrl: helper.basepath('ems.master/MstSentBackToUnderwriting.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstSentBackToCC', {
            url: '/MstSentBackToCC',
            title: 'MstSentBackToCC',
            templateUrl: helper.basepath('ems.master/MstSentBackToCC.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadApplicationView', {
            url: '/MstCadApplicationView',
            title: 'MstCadApplicationView',
            templateUrl: helper.basepath('ems.master/MstCadApplicationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCCRejectedApplications', {
            url: '/MstCCRejectedApplications',
            title: 'MstCCRejectedApplications',
            templateUrl: helper.basepath('ems.master/MstCCRejectedApplications.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstSanctionSummary', {
            url: '/MstSanctionSummary',
            title: 'MstSanctionSummary',
            templateUrl: helper.basepath('ems.master/MstSanctionSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstSanctionCheckerSummary', {
            url: '/MstSanctionCheckerSummary',
            title: 'MstSanctionCheckerSummary',
            templateUrl: helper.basepath('ems.master/MstSanctionCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstSanctionApprovalSummary', {
            url: '/MstSanctionApprovalSummary',
            title: 'MstSanctionApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstSanctionApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstSanctionDtlSummary', {
            url: '/MstSanctionDtlSummary',
            title: 'MstSanctionDtlSummary',
            templateUrl: helper.basepath('ems.master/MstSanctionDtlSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCreateSanction', {
            url: '/MstCreateSanction',
            title: 'MstCreateSanction',
            templateUrl: helper.basepath('ems.master/MstCreateSanction.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCADGroupProcessAssign', {
            url: '/MstCADGroupProcessAssign',
            title: 'MstCADGroupProcessAssign',
            templateUrl: helper.basepath('ems.master/MstCADGroupProcessAssign.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCADApplicationEdit', {
            url: '/MstCADApplicationEdit',
            title: 'MstCADApplicationEdit',
            templateUrl: helper.basepath('ems.master/MstCADApplicationEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstAppSanctionLetterGeneration', {
            url: '/MstAppSanctionLetterGeneration',
            title: 'MstAppSanctionLetterGeneration',
            templateUrl: helper.basepath('ems.master/MstAppSanctionLetterGeneration.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstAppSanctionLetterWordView', {
            url: '/MstAppSanctionLetterWordView',
            title: 'MstAppSanctionLetterWordView',
            templateUrl: helper.basepath('ems.master/MstAppSanctionLetterWordView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCADInstitutionDtlAdd', {
            url: '/MstCADInstitutionDtlAdd',
            title: 'MstCADInstitutionDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADInstitutionDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCADIndividualDtlAdd', {
            url: '/MstCADIndividualDtlAdd',
            title: 'MstCADIndividualDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADIndividualDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCADGroupDtlAdd', {
            url: '/MstCADGroupDtlAdd',
            title: 'MstCADGroupDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCADGroupDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })       
        .state('app.AtmMstChecklistMasterSummary', {
            url: '/AtmMstChecklistMasterSummary',
            title: 'AtmMstChecklistMasterSummary',
            templateUrl: helper.basepath('ems.audit/AtmMstChecklistMasterSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.AtmMstChecklistMasterAdd', {
            url: '/AtmMstChecklistMasterAdd',
            title: 'AtmMstChecklistMasterAdd',
            templateUrl: helper.basepath('ems.audit/AtmMstChecklistMasterAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.AtmMstChecklistMasterAudit', {
            url: '/AtmMstChecklistMasterAudit',
            title: 'AtmMstChecklistMasterAudit',
            templateUrl: helper.basepath('ems.audit/AtmMstChecklistMasterAudit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.AtmMstChecklistMasterAuditEdit', {
            url: '/AtmMstChecklistMasterAuditEdit',
            title: 'AtmMstChecklistMasterAuditEdit',
            templateUrl: helper.basepath('ems.audit/AtmMstChecklistMasterAuditEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmMstChecklistMasterAuditView', {
           url: '/AtmMstChecklistMasterAuditView',
           title: 'AtmMstChecklistMasterAuditView',
           templateUrl: helper.basepath('ems.audit/AtmMstChecklistMasterAuditView.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })                 
        .state('app.AtmMstAuditFrequency', {
           url: '/AtmMstAuditFrequency',
           title: 'AtmMstAuditFrequency',
           templateUrl: helper.basepath('ems.audit/AtmMstAuditFrequency.html?ver=' + version + '"'),
           resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.AtmMstAuditPriority', {
            url: '/AtmMstAuditPriority',
            title: 'AtmMstAuditPriority',
            templateUrl: helper.basepath('ems.audit/AtmMstAuditPriority.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.AtmMstPositiveConfirmity', {
            url: '/AtmMstPositiveConfirmity',
            title: 'AtmMstPositiveConfirmity',
            templateUrl: helper.basepath('ems.audit/AtmMstPositiveConfirmity.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstDocumentCheckList', {
            url: '/MstDocumentCheckList',
            title: 'MstDocumentCheckList',
            templateUrl: helper.basepath('ems.master/MstDocumentCheckList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadDocumentChecklistSummary', {
            url: '/MstCadDocumentChecklistSummary',
            title: 'MstCadDocumentChecklistSummary',
            templateUrl: helper.basepath('ems.master/MstCadDocumentChecklistSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadGuarantorDetails', {
            url: '/MstCadGuarantorDetails',
            title: 'MstCadGuarantorDetails',
            templateUrl: helper.basepath('ems.master/MstCadGuarantorDetails.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadDocumentChecklistAdd', {
            url: '/MstCadDocumentChecklistAdd',
            title: 'MstCadDocumentChecklistAdd',
            templateUrl: helper.basepath('ems.master/MstCadDocumentChecklistAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstIndividualDocCheckList', {
            url: '/MstIndividualDocCheckList',
            title: 'MstIndividualDocCheckList',
            templateUrl: helper.basepath('ems.master/MstIndividualDocCheckList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstGroupDocCheckList', {
            url: '/MstGroupDocCheckList',
            title: 'MstGroupDocCheckList',
            templateUrl: helper.basepath('ems.master/MstGroupDocCheckList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstDocumentTypeSummary', {
            url: '/MstDocumentTypeSummary',
            title: 'MstDocumentTypeSummary',
            templateUrl: helper.basepath('ems.master/MstDocumentTypeSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadLSASummary', {
            url: '/MstCadLSASummary',
            title: 'MstCadLSASummary',
            templateUrl: helper.basepath('ems.master/MstCadLSASummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadLSACheckerSummary', {
            url: '/MstCadLSACheckerSummary',
            title: 'MstCadLSACheckerSummary',
            templateUrl: helper.basepath('ems.master/MstCadLSACheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadLSAApprovalSummary', {
            url: '/MstCadLSAApprovalSummary',
            title: 'MstCadLSAApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstCadLSAApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadDeferralSummary', {
            url: '/MstCadDeferralSummary',
            title: 'MstCadDeferralSummary',
            templateUrl: helper.basepath('ems.master/MstCadDeferralSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadDeferralCheckerSummary', {
            url: '/MstCadDeferralCheckerSummary',
            title: 'MstCadDeferralCheckerSummary',
            templateUrl: helper.basepath('ems.master/MstCadDeferralCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadDeferralApprovalSummary', {
            url: '/MstCadDeferralApprovalSummary',
            title: 'MstCadDeferralApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstCadDeferralApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadCovenantSummary', {
            url: '/MstCadCovenantSummary',
            title: 'MstCadCovenantSummary',
            templateUrl: helper.basepath('ems.master/MstCadCovenantSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadCovenantCheckerSummary', {
            url: '/MstCadCovenantCheckerSummary',
            title: 'MstCadCovenantCheckerSummary',
            templateUrl: helper.basepath('ems.master/MstCadCovenantCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadCovenantApprovalSummary', {
            url: '/MstCadCovenantApprovalSummary',
            title: 'MstCadCovenantApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstCadCovenantApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadChequeManagementSummary', {
            url: '/MstCadChequeManagementSummary',
            title: 'MstCadChequeManagementSummary',
            templateUrl: helper.basepath('ems.master/MstCadChequeManagementSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadChequeMgmtCheckerSummary', {
            url: '/MstCadChequeMgmtCheckerSummary',
            title: 'MstCadChequeMgmtCheckerSummary',
            templateUrl: helper.basepath('ems.master/MstCadChequeMgmtCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadChequeMgmtApprovalSummary', {
            url: '/MstCadChequeMgmtApprovalSummary',
            title: 'MstCadChequeMgmtApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstCadChequeMgmtApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadNACHSummary', {
            url: '/MstCadNACHSummary',
            title: 'MstCadNACHSummary',
            templateUrl: helper.basepath('ems.master/MstCadNACHSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadNACHCheckerSummary', {
            url: '/MstCadNACHCheckerSummary',
            title: 'MstCadNACHCheckerSummary',
            templateUrl: helper.basepath('ems.master/MstCadNACHCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadNACHApprovalSummary', {
            url: '/MstCadNACHApprovalSummary',
            title: 'MstCadNACHApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstCadNACHApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadWaiverSummary', {
            url: '/MstCadWaiverSummary',
            title: 'MstCadWaiverSummary',
            templateUrl: helper.basepath('ems.master/MstCadWaiverSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadWaiverCheckerSummary', {
            url: '/MstCadWaiverCheckerSummary',
            title: 'MstCadWaiverCheckerSummary',
            templateUrl: helper.basepath('ems.master/MstCadWaiverCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadWaiverApprovalSummary', {
            url: '/MstCadWaiverApprovalSummary',
            title: 'MstCadWaiverApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstCadWaiverApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadDeviationSummary', {
            url: '/MstCadDeviationSummary',
            title: 'MstCadDeviationSummary',
            templateUrl: helper.basepath('ems.master/MstCadDeviationSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadDeviationCheckerSummary', {
            url: '/MstCadDeviationCheckerSummary',
            title: 'MstCadDeviationCheckerSummary',
            templateUrl: helper.basepath('ems.master/MstCadDeviationCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadDeviationApprovalSummary', {
            url: '/MstCadDeviationApprovalSummary',
            title: 'MstCadDeviationApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstCadDeviationApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadDisbursementRequestSummary', {
            url: '/MstCadDisbursementRequestSummary',
            title: 'MstCadDisbursementRequestSummary',
            templateUrl: helper.basepath('ems.master/MstCadDisbursementRequestSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadDisburseReqCheckerSummary', {
            url: '/MstCadDisburseReqCheckerSummary',
            title: 'MstCadDisburseReqCheckerSummary',
            templateUrl: helper.basepath('ems.master/MstCadDisburseReqCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadDisburseReqApprovalSummary', {
            url: '/MstCadDisburseReqApprovalSummary',
            title: 'MstCadDisburseReqApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstCadDisburseReqApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCheckerApprovalSummary', {
            url: '/MstCheckerApprovalSummary',
            title: 'MstCheckerApprovalSummary',
            templateUrl: helper.basepath('ems.idas/MstCheckerApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.DocDigitalSignatureSummary', {
            url: '/DocDigitalSignatureSummary',
            title: 'DocDigitalSignatureSummary',
            templateUrl: helper.basepath('ems.idas/DocDigitalSignatureSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.idasMstSanctionLetterWordView', {
            url: '/idasMstSanctionLetterWordView',
            title: 'idasMstSanctionLetterWordView',
            templateUrl: helper.basepath('ems.idas/idasMstSanctionLetterWordView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCheckerSummary', {
            url: '/MstCheckerSummary',
            title: 'MstCheckerSummary',
            templateUrl: helper.basepath('ems.idas/MstCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstSanctionEdit', {
            url: '/MstSanctionEdit',
            title: 'MstSanctionEdit',
            templateUrl: helper.basepath('ems.master/MstSanctionEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstSanctionWaiverSummary', {
            url: '/MstSanctionWaiverSummary',
            title: 'MstSanctionWaiverSummary',
            templateUrl: helper.basepath('ems.master/MstSanctionWaiverSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstGroupDocument', {
            url: '/MstGroupDocument',
            title: 'MstGroupDocument',
            templateUrl: helper.basepath('ems.master/MstGroupDocument.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstGroupWaiverSummary', {
            url: '/MstGroupWaiverSummary',
            title: 'MstGroupWaiverSummary',
            templateUrl: helper.basepath('ems.master/MstGroupWaiverSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
         .state('app.MstLANWaiverSummary', {
             url: '/MstLANWaiverSummary',
             title: 'MstLANWaiverSummary',
             templateUrl: helper.basepath('ems.master/MstLANWaiverSummary.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })
         .state('app.MstProgramAdd', {
                url: '/MstProgramAdd',
                title: 'MstProgramAdd',
                templateUrl: helper.basepath('ems.master/MstProgramAdd.html?ver=' + version + '"'),
                resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
         })
        .state('app.MstProgramEdit', {
            url: '/MstProgramEdit',
            title: 'MstProgramEdit',
            templateUrl: helper.basepath('ems.master/MstProgramEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstProgramView', {
             url: '/MstProgramView',
             title: 'MstProgramView',
             templateUrl: helper.basepath('ems.master/MstProgramView.html?ver=' + version + '"'),
             resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
           .state('app.TransUnionReport', {
               url: '/TransUnionReport',
               title: 'TransUnionReport',
               templateUrl: helper.basepath('ems.master/TransUnionReport.html?ver=' + version + '"'),
               resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
           })
        .state('app.BureauUpdateIndividual', {
            url: '/BureauUpdateIndividual',
            title: 'BureauUpdateIndividual',
            templateUrl: helper.basepath('ems.master/BureauUpdateIndividual.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.BureauUpdateInstitution', {
            url: '/BureauUpdateInstitution',
            title: 'BureauUpdateInstitution',
            templateUrl: helper.basepath('ems.master/BureauUpdateInstitution.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditInstitutionDtlAdd', {
            url: '/MstCreditInstitutionDtlAdd',
            title: 'MstCreditInstitutionDtlAdd',
            templateUrl: helper.basepath('ems.master/MstCreditInstitutionDtlAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCreditInstitutionBureauEdit', {
            url: '/MstCreditInstitutionBureauEdit',
            title: 'MstCreditInstitutionBureauEdit',
            templateUrl: helper.basepath('ems.master/MstCreditInstitutionBureauEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })

        .state('app.MstDigitalSignature', {
            url: '/MstDigitalSignature',
            title: 'MstDigitalSignature',
            templateUrl: helper.basepath('ems.master/MstDigitalSignature.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'ngWig', 'textAngular')
        })
        .state('app.MstCadDocChecklistCheckerSummary', {
            url: '/MstCadDocChecklistCheckerSummary',
            title: 'MstCadDocChecklistCheckerSummary',
            templateUrl: helper.basepath('ems.master/MstCadDocChecklistCheckerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadDocChecklistApprovalSummary', {
            url: '/MstCadDocChecklistApprovalSummary',
            title: 'MstCadDocChecklistApprovalSummary',
            templateUrl: helper.basepath('ems.master/MstCadDocChecklistApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCadGuarantorApproval', {
            url: '/MstCadGuarantorApproval',
            title: 'MstCadGuarantorApproval',
            templateUrl: helper.basepath('ems.master/MstCadGuarantorApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstDocumentSeverity', {
            url: '/MstDocumentSeverity',
            title: 'MstDocumentSeverity',
            templateUrl: helper.basepath('ems.master/MstDocumentSeverity.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstIndividualDocumentAdd', {
            url: '/MstIndividualDocumentAdd',
            title: 'MstIndividualDocumentAdd',
            templateUrl: helper.basepath('ems.master/MstIndividualDocumentAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstIndividualDocumentEdit', {
            url: '/MstIndividualDocumentEdit',
            title: 'MstIndividualDocumentEdit',
            templateUrl: helper.basepath('ems.master/MstIndividualDocumentEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstIndividualDocumentView', {
            url: '/MstIndividualDocumentView',
            title: 'MstIndividualDocumentView',
            templateUrl: helper.basepath('ems.master/MstIndividualDocumentView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCompanyDocumentAdd', {
            url: '/MstCompanyDocumentAdd',
            title: 'MstCompanyDocumentAdd',
            templateUrl: helper.basepath('ems.master/MstCompanyDocumentAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCompanyDocumentEdit', {
            url: '/MstCompanyDocumentEdit',
            title: 'MstCompanyDocumentEdit',
            templateUrl: helper.basepath('ems.master/MstCompanyDocumentEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstCompanyDocumentView', {
            url: '/MstCompanyDocumentView',
            title: 'MstCompanyDocumentView',
            templateUrl: helper.basepath('ems.master/MstCompanyDocumentView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })                 
        .state('app.MstGroupDocumentAdd', {
            url: '/MstGroupDocumentAdd',
            title: 'MstGroupDocumentAdd',
            templateUrl: helper.basepath('ems.master/MstGroupDocumentAdd.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstGroupDocumentEdit', {
            url: '/MstGroupDocumentEdit',
            title: 'MstGroupDocumentEdit',
            templateUrl: helper.basepath('ems.master/MstGroupDocumentEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        
        .state('app.MstGroupDocumentView', {
            url: '/MstGroupDocumentView',
            title: 'MstGroupDocumentView',
            templateUrl: helper.basepath('ems.master/MstGroupDocumentView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstApplcreationCICUploadView', {
            url: '/MstApplcreationCICUploadView',
            title: 'MstApplcreationCICUploadView',
            templateUrl: helper.basepath('ems.master/MstApplcreationCICUploadView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstApplcreationCICUploadInstView', {
            url: '/MstApplcreationCICUploadInstView',
            title: 'MstApplcreationCICUploadInstView',
            templateUrl: helper.basepath('ems.master/MstApplcreationCICUploadInstView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCreditIndividualBureauView', {
            url: '/MstCreditIndividualBureauView',
            title: 'MstCreditIndividualBureauView',
            templateUrl: helper.basepath('ems.master/MstCreditIndividualBureauView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCreditInstitutionBureauView', {
            url: '/MstCreditInstitutionBureauView',
            title: 'MstCreditInstitutionBureauView',
            templateUrl: helper.basepath('ems.master/MstCreditInstitutionBureauView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditCreationView', {
            url: '/AtmTrnAuditCreationView',
            title: 'AtmTrnAuditCreationView',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditCreationView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditCreationEdit', {
            url: '/AtmTrnAuditCreationEdit',
            title: 'AtmTrnAuditCreationEdit',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditCreationEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditCreationSummary', {
            url: '/AtmTrnAuditCreationSummary',
            title: 'AtmTrnAuditCreationSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditCreationSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnCheckpointSummary', {
            url: '/AtmTrnCheckpointSummary',
            title: 'AtmTrnCheckpointSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnCheckpointSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnCreateAudit', {
            url: '/AtmTrnCreateAudit',
            title: 'AtmTrnCreateAudit',
            templateUrl: helper.basepath('ems.audit/AtmTrnCreateAudit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnCheckpointObservation', {
            url: '/AtmTrnCheckpointObservation',
            title: 'AtmTrnCheckpointObservation',
            templateUrl: helper.basepath('ems.audit/AtmTrnCheckpointObservation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
         
        .state('app.AtmTrnMyAuditTaskSummary', {
            url: '/AtmTrnMyAuditTaskSummary',
            title: 'AtmTrnMyAuditTaskSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnMyAuditTaskSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnSampling', {
            url: '/AtmTrnSampling',
            title: 'AtmTrnSampling',
            templateUrl: helper.basepath('ems.audit/AtmTrnSampling.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnCheckpointSummaryAssign', {
            url: '/AtmTrnCheckpointSummaryAssign',
            title: 'AtmTrnCheckpointSummaryAssign',
            templateUrl: helper.basepath('ems.audit/AtmTrnCheckpointSummaryAssign.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnSamplingView', {
            url: '/AtmTrnSamplingView',
            title: 'AtmTrnSamplingView',
            templateUrl: helper.basepath('ems.audit/AtmTrnSamplingView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnMyAuditTask', {
            url: '/AtmTrnMyAuditTask',
            title: 'AtmTrnMyAuditTask',
            templateUrl: helper.basepath('ems.audit/AtmTrnMyAuditTask.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCCReport', {
            url: '/MstCCReport',
            title: 'MstCCReport',
            templateUrl: helper.basepath('ems.master/MstCCReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
		.state('app.HighmarkInstitutionReport', {
            url: '/HighmarkInstitutionReport',
            title: 'HighmarkInstitutionReport',
            templateUrl: helper.basepath('ems.master/HighmarkInstitutionReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.MstSentbackcctoCredit', {
            url: '/MstSentbackcctoCredit',
            title: 'MstSentbackcctoCredit',
            templateUrl: helper.basepath('ems.master/MstSentbackcctoCredit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstSentbackcctoCreditHistory', {
            url: '/MstSentbackcctoCreditHistory',
            title: 'MstSentbackcctoCreditHistory',
            templateUrl: helper.basepath('ems.master/MstSentbackcctoCreditHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.OsdTrnApprovalMyTicket',
        {
            url: '/OsdTrnApprovalMyTicket',
            title: 'OsdTrnApprovalMyTicket',
            templateUrl: helper.basepath('ems.osd/OsdTrnApprovalMyTicket.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.osdTrnApprovalPending360',
        {
            url: '/osdTrnApprovalPending360',
            title: 'osdTrnApprovalPending360',
            templateUrl: helper.basepath('ems.osd/osdTrnApprovalPending360.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.MstSentbackCadtoCcHistory', {
            url: '/MstSentbackCadtoCcHistory',
            title: 'MstSentbackCadtoCcHistory',
            templateUrl: helper.basepath('ems.master/MstSentbackCadtoCcHistory.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstVisitorManagementReport', {
            url: '/MstVisitorManagementReport',
            title: 'MstVisitorManagementReport',
            templateUrl: helper.basepath('ems.master/MstVisitorManagementReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

		.state('app.osdBankAlertReport', {
            url: '/osdBankAlertReport',
            title: 'osdBankAlertReport',
            templateUrl: helper.basepath('ems.osd/osdBankAlertReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

         .state('app.AtmTrnAssignedQuery', {
            url: '/AtmTrnAssignedQuery',
            title: 'AtmTrnAssignedQuery',
            templateUrl: helper.basepath('ems.audit/AtmTrnAssignedQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnReplyToQuery', {
            url: '/AtmTrnReplyToQuery',
            title: 'AtmTrnReplyToQuery',
            templateUrl: helper.basepath('ems.audit/AtmTrnReplyToQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnApproval',
        {
            url: '/AtmTrnApproval',
            title: 'AtmTrnApproval',
            templateUrl: helper.basepath('ems.audit/AtmTrnApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.AtmTrnApprovalView',
        {
            url: '/AtmTrnApprovalView',
            title: 'AtmTrnApprovalView',
            templateUrl: helper.basepath('ems.audit/AtmTrnApprovalView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.AtmTrnObservations', {
            url: '/AtmTrnObservations',
            title: 'AtmTrnObservations',
            templateUrl: helper.basepath('ems.audit/AtmTrnObservations.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnSampleAssignedQuery', {
            url: '/AtmTrnSampleAssignedQuery',
            title: 'AtmTrnSampleAssignedQuery',
            templateUrl: helper.basepath('ems.audit/AtmTrnSampleAssignedQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        .state('app.AtmTrnSampleReplyQuery', {
            url: '/AtmTrnSampleReplyQuery',
            title: 'AtmTrnSampleReplyQuery',
            templateUrl: helper.basepath('ems.audit/AtmTrnSampleReplyQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmMstChecklistMasterEdit', {
            url: '/AtmMstChecklistMasterEdit',
            title: 'AtmMstChecklistMasterEdit',
            templateUrl: helper.basepath('ems.audit/AtmMstChecklistMasterEdit.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditApprovalView', {
            url: '/AtmTrnAuditApprovalView',
            title: 'AtmTrnAuditApprovalView',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditApprovalView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditApproval', {
            url: '/AtmTrnAuditApproval',
            title: 'AtmTrnAuditApproval',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditApproval.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.MstCreditAddCovenantCheckList', {
            url: '/MstCreditAddCovenantCheckList',
            title: 'MstCreditAddCovenantCheckList',
            templateUrl: helper.basepath('ems.master/MstCreditAddCovenantCheckList.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.MstIndividualCovenantDocChecklist', {
            url: '/MstIndividualCovenantDocChecklist',
            title: 'MstIndividualCovenantDocChecklist',
            templateUrl: helper.basepath('ems.master/MstIndividualCovenantDocChecklist.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.MstGroupCovenantDocChecklist', {
            url: '/MstGroupCovenantDocChecklist',
            title: 'MstGroupCovenantDocChecklist',
            templateUrl: helper.basepath('ems.master/MstGroupCovenantDocChecklist.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives')
        })

        .state('app.AtmTrnAuditTaskSample', {
            url: '/AtmTrnAuditTaskSample',
            title: 'AtmTrnAuditTaskSample',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditTaskSample.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

		.state('app.MstTeleCallingReport', {
            url: '/MstTeleCallingReport',
            title: 'MstTeleCallingReport',
            templateUrl: helper.basepath('ems.master/MstTeleCallingReport.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
		})

        .state('page.CCMeetingApproval', {
            url: '/CCMeetingApproval',
            title: 'CCMeetingApproval',
            templateUrl: 'app/pages/CCMeetingApproval.html?ver=' + version + '"',
        })

        .state('app.AtmTrnAuditRaiseQuery', {
            url: '/AtmTrnAuditRaiseQuery',
            title: 'AtmTrnAuditRaiseQuery',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditRaiseQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })
        
        .state('app.AtmTrnSampleQuery', {
            url: '/AtmTrnSampleQuery',
            title: 'AtmTrnSampleQuery',
            templateUrl: helper.basepath('ems.audit/AtmTrnSampleQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.osdTrnTicketApprovalSummary', {
            url: '/osdTrnTicketApprovalSummary',
            title: 'osdTrnTicketApprovalSummary',
            templateUrl: helper.basepath('ems.osd/osdTrnTicketApprovalSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnMakerCheckpointObservation', {
            url: '/AtmTrnMakerCheckpointObservation',
            title: 'AtmTrnMakerCheckpointObservation',
            templateUrl: helper.basepath('ems.audit/AtmTrnMakerCheckpointObservation.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnMyAuditTaskAuditeeSummary', {
            url: '/AtmTrnMyAuditTaskAuditeeSummary',
            title: 'AtmTrnMyAuditTaskAuditeeSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnMyAuditTaskAuditeeSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnMyAuditTaskAuditeeView', {
            url: '/AtmTrnMyAuditTaskAuditeeView',
            title: 'AtmTrnMyAuditTaskAuditeeView',
            templateUrl: helper.basepath('ems.audit/AtmTrnMyAuditTaskAuditeeView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorMakerSummary', {
            url: '/AtmTrnAuditorMakerSummary',
            title: 'AtmTrnAuditorMakerSummary',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorMakerSummary.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')

        })

        .state('app.AtmTrnAuditorMakerView', {
            url: '/AtmTrnAuditorMakerView',
            title: 'AtmTrnAuditorMakerView',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorMakerView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditorRaiseQuery', {
            url: '/AtmTrnAuditorRaiseQuery',
            title: 'AtmTrnAuditorRaiseQuery',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditorRaiseQuery.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnSampleQueryAuditor', {
            url: '/AtmTrnSampleQueryAuditor',
            title: 'AtmTrnSampleQueryAuditor',
            templateUrl: helper.basepath('ems.audit/AtmTrnSampleQueryAuditor.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAuditCreationSampleView', {
            url: '/AtmTrnAuditCreationSampleView',
            title: 'AtmTrnAuditCreationSampleView',
            templateUrl: helper.basepath('ems.audit/AtmTrnAuditCreationSampleView.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        .state('app.AtmTrnAudit360View', {
            url: '/AtmTrnAudit360View',
            title: 'AtmTrnAudit360View',
            templateUrl: helper.basepath('ems.audit/AtmTrnAudit360View.html?ver=' + version + '"'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'localytics.directives', 'taginput', 'inputmask', 'textAngular')
        })

        
     
        
        // Horizontal layout
        // -----------------------------------
        // 
        // CUSTOM RESOLVES
        //   Add your own resolves properties
        //   following this object extend
        //   method
        // -----------------------------------
        // .state('app.someroute', {
        //   url: '/some_url',
        //   templateUrl: 'path_to_template.html',
        //   controller: 'someController',
        //   resolve: angular.extend(
        //     helper.resolveFor(), {
        //     // YOUR RESOLVES GO HERE
        //     }
        //   )
        // })
        ;

    } // routesConfig

    angular.module('angle').config(['$httpProvider', function ($httpProvider) {
        $httpProvider.interceptors.push(function ($q, $location, $rootScope, $cookies) {
            return {
                'request': function (config) {
                    config.headers.Authorization = $cookies.getObject('token');
                    //$http.defaults.headers.common.Authorization = localStorage.getItem("token");
                    return config;
                },
                'response': function (response) {
                    //Will only be called for HTTP up to 300
                    //console.log(response);
                    return response;
                },
                'responseError': function (rejection) {

                    if (rejection.status === 401) {
                        // $location.url('page/404?errno=401');
                    }
                    else if (rejection.status === 404) {
                        //console.log('404');
                        //$location.url('page/404?errno=404');
                    }
                    else if (rejection.status === 400) {
                        // $location.url('page/404?errno=400');
                    }
                    else if (rejection.status === 403) {
                        // $location.url('page/404?errno=403');
                    }
                    ////$state.go('page.404');
                    if (rejection.message === 404) {
                        //location.reload();
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }]);


})();

