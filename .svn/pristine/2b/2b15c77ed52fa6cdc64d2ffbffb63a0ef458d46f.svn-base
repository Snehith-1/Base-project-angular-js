'use strict';

/**
 * @ngdoc service
 * @name acmedApp.authInterceptorService
 * @description
 * # authInterceptorService
 * Factory in the acmedApp.
 */
angular.module('angle')
  .factory('authInterceptorService', ['$q', '$cookies', function ($q, $cookies) {

      var authInterceptorServiceFactory = {};
       

      authInterceptorServiceFactory.request = function (config) {
          //config.headers = config.headers || {};
          //var authData = $cookies.getObject('token');
          //if (authData) {
          //    config.headers.Authorization = authData;
          //}
          //return config;
      };
      authInterceptorServiceFactory.responseError = function (rejection) {
          if (rejection.status === 401) {
              //var authService = angular.injector(['user']).get('RefreshTokenService');
              //authService.refresh_token();
              $location.url('errorPage?errno=401');
              //var param = {
              //    previous_token: localStorage.getItem("token"),
              //    previous_refresh_token: localStorage.getItem("refresh_token")
              //};
              //SocketService.postlogin(apiManage.apiList['refresh_token'].api, param).then(function (response) {
              //    console.log(response);
              //});
          }
          else if (rejection.status === 404) {
              $location.url('errorPage?errno=404');
          }
          else if (rejection.status === 400) {
              $location.url('errorPage?errno=400');
          }
          else if (rejection.status === 403) {
              $location.url('errorPage?errno=403');
          }
          else if (rejection.status === 500) {
              var url = $location.path() + '?errno=500';
              $location.url(url);
          }
          return $q.reject(rejection);
      };
      return authInterceptorServiceFactory;
  }]);