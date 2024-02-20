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
        $urlRouterProvider.otherwise('/page/login');

        // 
        // Application Routes
        // -----------------------------------   
        $stateProvider
          .state('app', {
              url: '/app',
              abstract: true,
              templateUrl: helper.basepath('app.html'),
              resolve: helper.resolveFor('fastclick', 'modernizr', 'icons', 'screenfull', 'animo', 'sparklines', 'slimscroll', 'classyloader', 'toaster', 'whirl')
          })
          .state('app.welcome', {
              url: '/welcome',
              title: 'Welcome',
              templateUrl: helper.basepath('welcome.html')
          })

          .state('app.dashboard',
        {
            url: '/dashboard',
            title: 'dashboard',
            templateUrl: helper.basepath('dashboard.html'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'flot-chart', 'flot-chart-plugins', 'weather-icons')

        })
             .state('app.releaseManagement',
        {
            url: '/releaseManagement',
            title: 'releaseManagement',
            templateUrl: helper.basepath('releaseManagement.html'),
            resolve: helper.resolveFor('oitozero.ngSweetAlert', 'ui.select', 'ngTable', 'ngDialog', 'datatables', 'flot-chart', 'flot-chart-plugins', 'weather-icons')

        })
          //.state('app.dashboard', {
          //    url: '/dashboard',
          //    title: 'Dashboard',
          //    templateUrl: helper.basepath('dashboard.html'),
          //    resolve: helper.resolveFor('flot-chart', 'flot-chart-plugins', 'weather-icons'),
          //    resolve: helper.resolveFor('ngTable')
          //})
          //   .state('app.releaseManagement', {
          //       url: '/releaseManagement',
          //       title: 'releaseManagement',
          //       templateUrl: helper.basepath('releaseManagement.html'),
          //       resolve: helper.resolveFor('flot-chart', 'flot-chart-plugins', 'weather-icons'),
          //       resolve: helper.resolveFor('ngTable')
          //   })
            .state('app.readytorelease', {
                url: '/readytorelease',
                title: 'readytorelease',
                templateUrl: helper.basepath('readytorelease.html'),
                resolve: helper.resolveFor('flot-chart', 'flot-chart-plugins', 'weather-icons', 'datatables'),
                resolve: helper.resolveFor('ngTable')
            })

             .state('app.viewReleasePlan', {
                 url: '/viewReleasePlan',
                 title: 'viewReleasePlan',
                 templateUrl: helper.basepath('viewReleasePlan.html'),
                 resolve: helper.resolveFor('flot-chart', 'flot-chart-plugins', 'weather-icons', 'datatables'),
                 resolve: helper.resolveFor('ngTable')
             })

            .state('app.viewIssueDetails', {
                url: '/viewIssueDetails',
                title: 'viewIssueDetails',
                templateUrl: helper.basepath('viewIssueDetails.html'),
                resolve: helper.resolveFor('flot-chart', 'flot-chart-plugins', 'weather-icons', 'datatables'),
                resolve: helper.resolveFor('ngTable')
            })

            .state('app.responselog', {
                url: '/responselog',
                title: 'responselog',
                templateUrl: helper.basepath('responselog.html'),
                resolve: helper.resolveFor('flot-chart', 'flot-chart-plugins', 'weather-icons')
            })

          // pages

          .state('page', {
              url: '/page',
              templateUrl: 'app/pages/page.html',
              resolve: helper.resolveFor('modernizr', 'icons'),
              controller: ['$rootScope', function ($rootScope) {
                  $rootScope.app.layout.isBoxed = false;
              }]
          })
          .state('page.login', {
              url: '/login',
              title: 'Login',
              templateUrl: 'app/pages/login.html'
          })
          .state('page.register', {
              url: '/register',
              title: 'Register',
              templateUrl: 'app/pages/register.html'
          });

    } // routesConfig

    angular.module('vcx').config(['$httpProvider', function ($httpProvider) {
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
                    unlockUI();
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

