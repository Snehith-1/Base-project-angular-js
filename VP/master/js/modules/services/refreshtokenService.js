'use strict';
angular.module('vcx').factory('RefreshTokenService', ['apiManage', 'SocketService',
    function (apiManage, SocketService) {
        var service = {};
        service.refresh_token = function () {
            var param = {
                previous_token: localStorage.getItem("token_bc"),
                previous_refresh_token: localStorage.getItem("refresh_token")
            };
            return SocketService.postlogin(apiManage.apiList['refresh_token'].api, param).then(function (response) {                
                return response;
            });
        }
        return service;
    }
]);